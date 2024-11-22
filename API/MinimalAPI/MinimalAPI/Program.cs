
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Data;
using MinimalAPI.Models;

namespace MinimalAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var app = builder.Build();

            app.MapGet("/GetAll", (ApplicationDbContext context) => {
                var employees = context.Employees.ToList();
                return Results.Ok(employees);
            });

            app.MapGet("/Get", (ApplicationDbContext context, [FromQuery] int Id) =>
            {
                var employee = context.Employees.Find(Id);
                return Results.Ok(employee);
            });

            app.MapPost("/", (ApplicationDbContext context, Employee employee) =>
            {
                context.Employees.Add(employee);
                context.SaveChanges();
                return Results.Ok(employee);
            });
            app.MapPut("/", (ApplicationDbContext context,Employee emp) => {
                var employee = context.Employees.Find(emp.Id);
                employee.Name = emp.Name;
                employee.Description = emp.Description;    
                context.SaveChanges();
                return Results.Ok("Success");

            });

            app.MapDelete("/", (ApplicationDbContext context,[FromQuery] int Id) => {
                var employee = context.Employees.Find(Id);
                context.Employees.Remove(employee);
                context.SaveChanges();
                return Results.Ok("Success");
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            app.MapGet("/weatherforecast", (HttpContext httpContext) =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = summaries[Random.Shared.Next(summaries.Length)]
                    })
                    .ToArray();
                return forecast;
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi();

            app.Run();
        }
    }
}
