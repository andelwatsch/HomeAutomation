using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ioBroker.net;
using SmartHome.Automation;
using SmartHome.Entities;
using SmartHome.Services;
using SmartHome.Settings;

namespace SmartHome
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            MapConfiguration();
        }

        public IConfiguration Configuration { get; }

        private void MapConfiguration()
        {
            MapIoBrokerHostSettings();
        }

        private void MapIoBrokerHostSettings()
        {
            var brokerHostSettings = new IoBrokerSettings();
            Configuration.GetSection(nameof(IoBrokerSettings)).Bind(brokerHostSettings);
            AppSettingsProvider.IoBrokerHostSettings = brokerHostSettings;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartHome", Version = "v1" });
            });


            services.AddSingleton<IIoBrokerDotNet>(new IoBrokerDotNet(
                $"{AppSettingsProvider.IoBrokerHostSettings.Host}:{AppSettingsProvider.IoBrokerHostSettings.Port}"));
            services.AddEntities();
            services.AddHostedStartupServices();
            services.AddAutomationHostedServices();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartHome v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
