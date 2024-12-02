
using FluentValidation;
using Identity_Jwt.Data;
using Identity_Jwt.Dto_s.Departments;
using Identity_Jwt.Dto_s.Employees;
using Identity_Jwt.Dto_s.Identity;
using Identity_Jwt.Errors;
using Identity_Jwt.Identity;
using Identity_Jwt.Models;
using Identity_Jwt.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Identity_Jwt
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
            builder.Services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
            builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("UserConnection"))
            );
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

            builder.Services.AddScoped<IValidator<EmployeesDto>, EmployeesDtoValidation>();

            builder.Services.AddScoped<IValidator<DepartmentsDto>, DepartmentsDtoValidation>();
            builder.Services.AddScoped<IValidator<RegisterDto>, RegisterDtoValidation>();   
            builder.Services.AddScoped<IValidator<SigninDto>, SigninDtoValidation>();
            builder.Services.AddScoped<AuthServices>();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(options => 
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = "Token Maker",
                ValidAudience = "Website Users",
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("jwt")["SecretKey"]))
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseExceptionHandler(options => { });
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
