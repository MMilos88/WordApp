using AutoMapper;
using DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using WordApp.CommonInterface;
using WordApp.Mapper;
using WordApp.Middlewares;
using WordApp.Repository;
using WordApp.Resources;

namespace WordApp
{
    public class Startup
    {
        private readonly IConfiguration config;
        private readonly string MyAllowAnyOrigins = "Allow_Any";

        public Startup(IConfiguration config)
        {
            this.config = config;           
        }

        public IConfiguration Configuration { get; }
      

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TextDbContext>(options => options.UseSqlServer(config.GetConnectionString("SampleDatabase")));

            services.AddLocalization(o =>
            {
                // We will put our translations in a folder called Resources
                o.ResourcesPath = "Resources";
            });

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new Maper());
            });
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowAnyOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin()
                     .AllowAnyHeader()
                     .AllowAnyMethod();
                });
            });
       
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Version = "v1",
                    Title = "WordApp API",
                    Description = "WordApp Web API",
                    TermsOfService = "None",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "Milos Mijatovic", Email = "Milos.Mijatovic88@gmail.com" }
                });
            });

            services.AddSingleton<ISharedLocalizer, SharedResource>();

            return AutofacConfigurator.ConfigureAutofacDI(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ErrorLoggingMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(MyAllowAnyOrigins);

            IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("sr-Latn-RS"),
                new CultureInfo("ru-RU"),
                new CultureInfo("es-es")
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WordApp API V1"));
        }
    }
}


