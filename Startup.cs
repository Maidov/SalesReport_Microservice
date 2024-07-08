using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using System;
using ReportApi.Models;

namespace ReportApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SalesContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddSingleton<KafkaProducerService>();
            services.AddHostedService<KafkaConsumerService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "SalesReportAPI",
                    Description = "API that allows you to get monthly reports",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Югослав Добровольский",
                        Email = "heavenmaido@gmail.com",
                    }
                });
            });

            services.AddControllers();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Report API V1");
                    c.RoutePrefix = string.Empty; 
                });
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}