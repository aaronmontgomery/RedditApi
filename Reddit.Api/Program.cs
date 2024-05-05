using System.Text;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using Services;
using Models;

namespace Reddit.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.Configure<SettingsOptionsModel>(builder.Configuration.GetSection(SettingsOptionsModel.Settings));

            builder.Services.Configure<RedditApiSettingsOptionsModel>(builder.Configuration.GetSection(RedditApiSettingsOptionsModel.RedditApiSettings));

            builder.Services.AddHttpClient("RedditApiAuthHttpClient", x =>
            {
                string? baseUrl = builder.Configuration.GetValue<string>("RedditApiSettings:RedditApiBaseUrl");
                string? username = builder.Configuration.GetValue<string>("RedditApiSettings:RedditApiUsername");
                string? password = builder.Configuration.GetValue<string>("RedditApiSettings:RedditApiPassword");
                string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
                x.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                x.DefaultRequestHeaders.UserAgent.ParseAdd("Reddit.Api/v1 (by /u/aaronmontgomery2809)");
                x.BaseAddress = new Uri(baseUrl!);
            });

            builder.Services.AddHttpClient("RedditApiOauthHttpClient", x =>
            {
                string? baseUrl = builder.Configuration.GetValue<string>("RedditApiSettings:RedditApiOauthBaseUrl");
                x.DefaultRequestHeaders.UserAgent.ParseAdd("Reddit.Api/v1 (by /u/aaronmontgomery2809)");
                x.BaseAddress = new Uri(baseUrl!);
            });

            builder.Services.AddSingleton<IRedditAuthService, RedditAuthService>();

            builder.Services.AddScoped<IRedditRedirectService, RedditRedirectService>();

            builder.Services.AddScoped<IRedditService, RedditService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var webSocketOptions = new WebSocketOptions
            {
                KeepAliveInterval = TimeSpan.FromMinutes(2)
            };

            webSocketOptions.AllowedOrigins.Add("*");

            app.UseWebSockets(webSocketOptions);

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapGet("/reddit", async context =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    IRedditService redditService = context.RequestServices.GetRequiredService<IRedditService>();
                    while (!context.RequestAborted.IsCancellationRequested)
                    {
                        PopularModel? popularModel = await redditService.Get<PopularModel>(redditService.HttpClient, builder.Configuration.GetValue<string>("RedditApiSettings:RedditApiOauthPopularUrl")!);
                        if (popularModel is not null && popularModel.Data is not null && popularModel.Data.Childrens is not null)
                        {
                            foreach (Children children in popularModel.Data.Childrens)
                            {
                                if (children.Data is not null && children.Data.Description is not null)
                                {
                                    await Task.Delay(3500, context.RequestAborted);
                                    await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(children.Data.Description)), WebSocketMessageType.Text, true, context.RequestAborted);
                                }
                            }
                        }
                    }
                }

                else
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Expected a WebSocket request", context.RequestAborted);
                }
            });
            
            app.Run();
        }
    }
}
