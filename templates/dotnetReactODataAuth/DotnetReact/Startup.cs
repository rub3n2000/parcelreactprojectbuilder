using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData.Edm;
using Microsoft.AspNet.OData.Builder;
using DotnetReact.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using DotnetReact.Models;

namespace DotnetReact
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
            services.AddDbContext<DataContext>(x =>
            {
                x.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddAutoMapper(typeof(AuthRepository).Assembly);
            
            services.AddScoped<IAuthRepository, AuthRepository>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                options =>
                {
                    options.IncludeErrorDetails = true;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                           System.Text.Encoding.UTF8.GetBytes(Configuration.GetValue<string>("Token"))
                        ),
                        ValidateAudience = true,
                        ValidAudience = "Auth",
                        ValidIssuer = "Auth",
                        RequireSignedTokens = true,
                        RequireExpirationTime = true, // <- JWTs are required to have "exp" property set
                        ValidateLifetime = true, // <- the "exp" will be validated
                        ValidateIssuer = true
                    };
                }
            );

            services.AddControllers(options => options.UseGeneralRoutePrefix("/api"));
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.Select().Filter();
                endpoints.MapODataRoute("odata", "api", GetEdmModel());
                endpoints.MapControllerRoute(name: "default",
                            pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapFallbackToFile("index.html");
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
