
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Workshop.Core.Interfaces;
using Workshop.Core.Models;
using Workshop.Infrastructure.Data;
using Workshop.Infrastructure.Repositories;

namespace Workshop.API
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

            builder.Services.AddDbContext<AppDbContext>(
            options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnections"));
            });

            builder.Services.AddIdentity<Users, IdentityRole<int>>(
                options =>
                {
                    options.Password.RequiredUniqueChars = 1;
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                }).AddEntityFrameworkStores<AppDbContext>();

            //builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            //builder.Services.AddScoped<IItemsRepository, ItemsRepository>();
            //builder.Services.AddScoped<ICartRepository, CartRepository>();
            //builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            var Config = TypeAdapterConfig.GlobalSettings;
            builder.Services.AddSingleton(Config);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
