
using Dal;
using Dal.IServices;
using Dal.IRepositories;
using Dal.Repositories;
using Dal.Services;
using Microsoft.EntityFrameworkCore;

namespace MaCaveAVin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Services
            builder.Services.AddScoped<ICellarRepository, CellarRepository>();
            builder.Services.AddScoped<IAgeValidationService, AgeValidationService>();
            builder.Services.AddScoped<IPositionService, PositionService>();
            builder.Services.AddScoped<IPeakService, PeakService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IBottleRepository, BottleRepository>();

            builder.Services.AddControllers().AddJsonOptions(options =>
                                 {
                                     options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                                 });

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
