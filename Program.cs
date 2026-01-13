
using coffee_machine_api.Services;
using System.Globalization;

namespace coffee_machine_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddHttpClient<WeatherService>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            int callCount = 0;


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();


            app.MapGet("/brew-coffee", async (WeatherService weatherService) =>
            {
                // April 1st
                if (DateTime.Now.Month == 4 && DateTime.Now.Day == 1)
                    return Results.StatusCode(418);

                callCount++;

                // Every 5th call
                if (callCount % 5 == 0)
                    return Results.StatusCode(503);

                var temperature = await weatherService.GetCurrentTemperatureAsync();

                var message = temperature > 30
                    ? "Your refreshing iced coffee is ready"
                    : "Your piping hot coffee is ready";

                return Results.Ok(new
                {
                    message,
                    prepared = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")
                });
            });

            app.Run();
        }
    }
}
