﻿
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApi.Store;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNetCore.Mvc.Cors.Internal;

namespace WebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            // Add framework services.
            services.AddMvc();

            services.AddSwaggerGen(
                config =>
                {
                    config.SwaggerDoc("v1", new Info()
                    {
                        Version = "v1",
                        Title = "Users Web API",
                        Description = "API to load user information",
                        TermsOfService = "None"
                    });

                    config.IncludeXmlComments(GetXmlCommentsPath());
                    config.DescribeAllEnumsAsStrings();
                }
            );

            // Register a transient service for DI
            services.AddTransient<IUserDataStore>(
                userProvider =>
                {
                    var dbSection = Configuration.GetSection("Database");
                    return new UserDocumentStore(dbSection.GetValue<string>("endpoint"), dbSection.GetValue<string>("authKey"),
                        dbSection.GetValue<string>("ID"), dbSection.GetValue<string>("Collection"));
                }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseCors("AllowAll");

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "User Web API");

                    options.RoutePrefix = "api/docs";

                    options.ShowJsonEditor();
                }
            );
        }

        private string GetXmlCommentsPath()
        {
            var app = PlatformServices.Default.Application;
            return System.IO.Path.Combine(app.ApplicationBasePath, $"{System.Reflection.Assembly.GetEntryAssembly().GetName().Name}.xml");
        }
    }
}
