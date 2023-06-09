using DWShop.Application.Extensions;
using DWShop.Domain.Entities;
using DWShop.Infrastructure.Context;
using DWShop.Infrastructure.Extensions;
using DWShop.Service.Api.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DWShop.Service.Api
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
            builder.Services.AddApplicationLayer();
            builder.Services.AddRepositories();

            builder.Services.AddDbContext<DWShopContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("Identity:Key")!);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });


            builder.Services.AddIdentity<DWUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            })
                .AddRoles<IdentityRole>()
                .AddSignInManager<SignInManager<DWUser>>()
                .AddRoleValidator<RoleValidator<IdentityRole>>()
                .AddEntityFrameworkStores<DWShopContext>();
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

/*
 * 
 * 1.- use los principos de la arquitectura limpia ☻
 * 2.- queremos implementar los siguientes movimientos
 * 3.- Catalogo de productos, Canasta de productos, Lista de checkout
 * 4.- queremos tener campos de auditoria automatica
 * 5.- queremos tener una tabla de auditoria profunda
 * 6.- usar SQL Server con EntityFramework ☻
 * 7.- y utilizar los siguientes patrones de diseño
 *     CQRS, Repository, Unit of work ♥
 * 8.- modular, no dependencias y no codigo espagetti    
 * 
 * */