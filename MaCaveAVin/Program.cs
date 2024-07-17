using Dal;
using Dal.IServices;
using Dal.IRepositories;
using Dal.Repositories;
using Dal.Services;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace MaCaveAVin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Repositories
            builder.Services.AddScoped<ICellarRepository, CellarRepository>();
            builder.Services.AddScoped<ICellarCategoryRepository, CellarCategoryRepository>();
            builder.Services.AddScoped<ICellarModelRepository, CellarModelRepository>();

            // Services
            builder.Services.AddScoped<IAgeValidationService, AgeValidationService>();
            builder.Services.AddScoped<IBottlePositionService, PositionService>();
            builder.Services.AddScoped<IPeakService, PeakService>();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });

            builder.Services.AddDbContext<CellarContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("CellarDatabase")));

            builder.Services.AddAuthorization();

            builder.Services.AddIdentityApiEndpoints<AppUser>(c => { c.Password.RequiredLength = 6; })
                .AddEntityFrameworkStores<CellarContext>();
              //  .AddRoles<IdentityRole>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapIdentityApi<AppUser>();

            app.MapControllers(); // This line maps controller actions as endpoints.

            app.Run();
        }
    }
}