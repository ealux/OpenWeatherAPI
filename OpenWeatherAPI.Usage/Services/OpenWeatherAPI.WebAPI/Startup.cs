using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using OpenWeatherAPI.DAL.Context;
using OpenWeatherAPI.DAL.Entities;
using OpenWeatherAPI.DAL.Repositories;
using OpenWeatherAPI.Interfaces.Base.Repositories;
using OpenWeatherAPI.WebAPI.Data;

namespace OpenWeatherAPI.WebAPI
{
    public record Startup(IConfiguration configuration)
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // DB
            services.AddDbContext<DataDB>(
                opt => opt
                .UseSqlServer(
                    configuration.GetConnectionString("Data"),
                    o => o.MigrationsAssembly("OpenWeatherAPI.DAL.SqlServer")));
            services.AddTransient<DataDBInitializer>();

            // Controllers
            services.AddControllers();

            //Repositories
            services.AddTransient<IRepository<DataSource>, DBRepository<DataSource>>();

            services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
                .AddNegotiate();

            services.AddAuthorization(options => options.FallbackPolicy = options.DefaultPolicy);
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataDBInitializer db)
        {
            db.Initialize();

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}