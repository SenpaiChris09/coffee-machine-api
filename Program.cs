
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


            app.MapGet("/brew-coffee", () =>
            {
                // April 1st check
                if (DateTime.Now.Month == 4 && DateTime.Now.Day == 1)
                {
                    return Results.StatusCode(418);
                }

                callCount++;

                // Every 5th call
                if (callCount % 5 == 0)
                {
                    return Results.StatusCode(503);
                }


                return Results.Ok(new
                {
                    message = "Your piping hot coffee is ready",
                    prepared = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture)
                });
            });

            app.Run();
        }
    }
}
