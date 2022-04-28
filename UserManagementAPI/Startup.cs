using AutoMapper;
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
using UserManagementAPI.AutoMapper;
using UserManagementAPI.Services;

namespace UserManagementAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("1.0", new OpenApiInfo
                {
                    Title = "User Management API",
                    Version = "1.0",
                    Description = "User Management API"
                });
            });
            //Register Automapper
            var autoMapper = new MapperConfiguration(mc => mc.AddProfile(new AutomapperProfiler())).CreateMapper();
            services.AddSingleton(autoMapper);

            services.AddHealthChecks();
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/1.0/swagger.json", "UserManagementAPI");
                c.DisplayOperationId();
                c.DisplayRequestDuration();
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //Enable health
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/healtz");
            });
        }
    }
}
