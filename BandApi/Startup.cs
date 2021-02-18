using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BandApi.Data;
using BandApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace BandApi
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
            services.AddResponseCaching();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers(
                setupoptions=>
                {
                    setupoptions.ReturnHttpNotAcceptable = true;
                    setupoptions.CacheProfiles.Add("90SecondsCacheProfile",
                        new CacheProfile { Duration = 90 });
                })
                  .AddNewtonsoftJson(setupAction=>{
                     setupAction.SerializerSettings.ContractResolver=
                     new CamelCasePropertyNamesContractResolver();
                  })
                  .AddXmlDataContractSerializerFormatters();
            services.AddCors(
                Options=>{
                    Options.AddDefaultPolicy(Builder=>Builder.WithOrigins("http://localhost:3000"));
                    Options.AddPolicy("mypolicy",Builder=>Builder.WithOrigins("http://192.168.1.38:3000"));
                } 
                
            );
            
            services.AddScoped<IBandAlbumnRepo, BandAlbumnRepo>();
            services.AddScoped<IpropertyValidationService, propertyValidationService>();
            services.AddScoped<IPropertyMappingService, PropertyMappingService>();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(ApplicationBuilder=>{
                    ApplicationBuilder.Run(async c=>{
                        c.Response.StatusCode=500;
                        await c.Response.WriteAsync("Something went wrong try again");
                    });
                });
            }
            app.UseResponseCaching();

            app.UseRouting();
            app.UseCors("mypolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors("mypolicy");
            });
        }
    }
}
