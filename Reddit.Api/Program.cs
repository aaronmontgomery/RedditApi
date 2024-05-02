
using Services;

namespace Reddit.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddHttpClient(nameof (RedditAuthService), x =>
            {

            });
            
            builder.Services.AddSingleton<IWebSocketHandlerService, WebSocketHandlerService>();
            
            builder.Services.AddSingleton<IRedditAuthService, RedditAuthService>();
            
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