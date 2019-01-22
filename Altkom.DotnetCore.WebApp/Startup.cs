using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Altkom.DotnetCore.FakeServices;
using Altkom.DotnetCore.IServices;
using Altkom.DotnetCore.WebApp.Formatters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Yaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Altkom.DotnetCore.WebApp
{
    public class StartupDevelopment
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
    
        }

    }

    public class StartupProduction
    {
        public void ConfigureServices(IServiceCollection services)
        {
            
         // ...
         }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
    //    ...
        }
    }




    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                .AddXmlFile($"appsettings.xml", optional: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddYamlFile("appsettings.yml", optional: true)
                .AddEnvironmentVariables()
                ;

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }
            Configuration = builder.Build();


        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICustomerService, FakeCustomerService>();
            services.AddScoped<CustomerFaker>();
            services.AddScoped<IProductService, FakeProductService>();
            services.AddScoped<ProductFaker>();

            // add package Microsoft.AspNetCore.Mvc.Formatters.Xml
            services
                .AddMvc(options =>
                    {
                        options.RespectBrowserAcceptHeader = true;
                        options.OutputFormatters.Add(new VcardOutputFormatter());
                    }
                )
                .AddXmlSerializerFormatters()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // add package Swashbuckle.AspNetCore
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });

                var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlfile);

                c.IncludeXmlComments(xmlPath);
            });


            //services
            //    .AddMvc(options =>
            //    {
            //        options.RespectBrowserAcceptHeader = true; // domyślnie ustawione na false
            //        options.OutputFormatters.Add(new XmlSerializerOutputFormatter());                   
            //    })


            var option1 = Configuration.GetSection("Option1").Value;

            var option2 = Configuration.GetSection("Options:Option2").Value;

            services.AddOptions();
            services.Configure<CustomerServiceOptions>(Configuration.GetSection("MyConfig"));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsProduction())
            {

                app.Use(async (context, next) =>
                {
                    if (context.Request.Headers.ContentLength > 1000)
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;

                        await context.Response.WriteAsync("Message exceed limit");

                        return;

                    }
                    await next();
                });
            }
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
               app.UseHsts();
            }

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseSwagger();

           

           

            // app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}