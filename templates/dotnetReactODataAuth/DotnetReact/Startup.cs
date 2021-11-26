using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData.Edm;
using Microsoft.AspNet.OData.Builder;
using DotnetReact.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using DotnetReact.Models;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace DotnetReact
{
    public class Startup
    {
        private const string ROUTE_PREFIX = "";

        private readonly string[] FRONTEND_ROUTES = {
            $"{ROUTE_PREFIX}",
            $"{ROUTE_PREFIX}/"
        };

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(x =>
            {
                x.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddAutoMapper(typeof(AuthRepository).Assembly);
            
            services.AddScoped<IAuthRepository, AuthRepository>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                options  =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters{
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                           System.Text.Encoding.ASCII.GetBytes(Configuration.GetValue<string>("Token"))
                        ),
                        ValidateAudience = false,
                        ValidateIssuer = false
                    };
                }
            );

            services.AddControllers(mvcOptions => 
                mvcOptions.EnableEndpointRouting = false);
            services.AddOData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            
            //app.UseHttpsRedirection();
            app.UseRouting();
            
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseMvc(routeBuilder =>
            {
                routeBuilder.Select().Filter();
                routeBuilder.MapODataServiceRoute("odata", "api", GetEdmModel());
                routeBuilder.MapRoute(name: "default",
                            template: "api/{controller}/{action}/{id?}");
                routeBuilder.MapSpaFallbackRoute("spa", defaults: new { controller = "Home", action = "Index" });
                routeBuilder.EnableDependencyInjection();
            });

            IEdmModel GetEdmModel()
            {
                var odataBuilder = new ODataConventionModelBuilder();
                odataBuilder.EntitySet<WeatherForecast>("WeatherForecast");
                odataBuilder.EntitySet<User>("Users");
                return odataBuilder.GetEdmModel();
            }
            
        }
    }
}
