using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using DotnetReact.Data;
using AutoMapper;
using System.Security.Cryptography;
using System;

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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                options =>
                {
                    RSA rsa = RSA.Create();
                    rsa.ImportRSAPublicKey(
                        source: Convert.FromBase64String(Configuration.GetValue<string>("JwtPublic")),
                        bytesRead: out int _
                    );
                    
                    options.IncludeErrorDetails = true;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        IssuerSigningKey = new RsaSecurityKey(rsa),
                        ValidAudience = "Auth",
                        ValidIssuer = "Auth",
                        RequireSignedTokens = true,
                        RequireExpirationTime = true, // <- JWTs are required to have "exp" property set
                        ValidateLifetime = true, // <- the "exp" will be validated
                        ValidateAudience = true,
                        ValidateIssuer = true,
                    };
                }
            );
            services.AddControllers();
            services.AddControllersWithViews(o =>
            {
                o.UseGeneralRoutePrefix("api/");
            });

            services.AddDbContext<DataContext>(x =>
            {
                x.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddAutoMapper(typeof(AuthRepository).Assembly);

            services.AddScoped<IAuthRepository, AuthRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            //app.UseHttpsRedirection();
            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default",
                            pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
