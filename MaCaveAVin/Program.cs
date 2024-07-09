
using Dal;
using MaCaveAVin.Interfaces;
using MaCaveAVin.Services;
using Microsoft.EntityFrameworkCore;

namespace MaCaveAVin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IAgeValidationService, AgeValidationService>();

            // Ajouter les services de contrôleur
            builder.Services.AddControllers();
            builder.Services.AddDbContext<CellarContext>(options =>
                                    options.UseSqlServer(builder.Configuration.GetConnectionString("CellarDatabase")));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
