using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using System;
using TodoApi.Models;

namespace TodoApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Этот метод вызывается во время выполнения. Используется для добавления сервисов в контейнер.
        public void ConfigureServices(IServiceCollection services)
        {
            // Регистрация контекста базы данных для PostgreSQL
            services.AddDbContext<SalesContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            // Добавление сервиса Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Todo API",
                    Description = "A simple ASP.NET Core Web API for Sales Reporting",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Your Name",
                        Email = "yourname@example.com",
                        Url = new Uri("https://example.com"),
                    }
                });
            });

            // Добавление контроллеров API
            services.AddControllers();
        }

        // Этот метод вызывается во время выполнения. Используется для настройки конвейера HTTP-запроса.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Включение использования Swagger UI в режиме разработки
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
                    c.RoutePrefix = string.Empty; // Префикс маршрута Swagger UI (пустой для корня сервера)
                });
            }

            // Подключение маршрутизации контроллеров API
            app.UseRouting();

            // Включение использования контроллеров API
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}