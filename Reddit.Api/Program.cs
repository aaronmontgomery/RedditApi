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
                    var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    var redditService = context.RequestServices.GetRequiredService<IRedditService>();
                    
                    while (true)
                    {
                        PopularModel? popularModel = await redditService.GetSubRedditAsync(redditService.HttpClient);
                        if (popularModel is not null && popularModel.Data is not null && popularModel.Data.Childrens is not null)
                        {
                            foreach (Children children in popularModel.Data.Childrens)
                            {
                                if (children.Data is not null && children.Data.Description is not null)
                                {
                                    await Task.Delay(3100);
                                    await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(children.Data.Description)), WebSocketMessageType.Text, true, CancellationToken.None);
                                }
                            }
                        }
                    }
                }

                else
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Expected a WebSocket request");
                }
            });
            
            app.Run();
        }
    }
}
