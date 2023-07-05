using DWShop.Application.Extensions;
using DWShop.Application.Interfaces.Services;
using DWShop.Domain.Entities;
using DWShop.Infrastructure.Context;
using DWShop.Infrastructure.Extensions;
using DWShop.Service.Api.Middlewares;
using DWShop.Service.Api.Services;
using DWShop.Shared.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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


            builder.Services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Tienda en linea DW", Version = "V1" });

                x.AddSecurityDefinition(StorageConstants.Local.Scheme, new OpenApiSecurityScheme
                {
                    Description = @"Coloca aqui tu token usando el esquema Bearer por ejemplo Bearer asdiajshdfklasjhfklaj",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = StorageConstants.Local.Scheme
                });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                     new OpenApiSecurityScheme
                     {
                        Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = StorageConstants.Local.Scheme,

                     },

                     Scheme = "oauth2",
                     Name = StorageConstants.Local.Scheme,
                     In = ParameterLocation.Header

                     },
                     new List<string>()
                    }
                });
            });


            builder.Services.AddApplicationLayer();
            builder.Services.AddRepositories();
            builder.Services.AddScoped<ICurrentUserService, CurrentUserServices>();
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
     
                app.UseSwagger();
                app.UseSwaggerUI();
           

            app.UseAuthentication();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseAuthorization();

            app.MapControllers();

            app.UseCors(x => x.AllowAnyMethod()
                             .AllowAnyOrigin()
                             .AllowAnyHeader());

            app.Run();
        }
    }
}

