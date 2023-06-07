using DWShop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using DWShop.Application.Extensions;
using DWShop.Infrastructure.Extensions;

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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

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