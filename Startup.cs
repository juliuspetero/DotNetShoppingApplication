using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShoppingApplication.Interfaces;
using ShoppingApplication.Models;
using ShoppingApplication.Repositories;
using XentePaymentSDK;

namespace ShoppingApplication
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
            #region Associating the context class with the connection string
            services.AddDbContextPool<AppDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ShoppingApplicationDBConnection")));
            #endregion

            #region Configuration application user with the role
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Set minimal password to reduce complexity
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                // The Identity framework uses the AppDbContext
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            #endregion

            #region Add Authentication using JWT instead of session
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                // The options parameters allows you manipulate how the token object should be constructed 
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        // A string to check for validity
                        ValidAudience = this.Configuration["Jwt:Site"],
                        ValidateIssuer = true,
                        // A string to check for the validity
                        ValidIssuer = this.Configuration["Jwt:Site"],
                        // The sign key must be converted into bytes
                        IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["Jwt:Signingkey"])),
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };
                });
            #endregion


            #region Configure MVC design pattern
            services.AddMvc(options =>
            {
                // Set all the controllers and methods to be accessed by only loggedin users
                var policy = new AuthorizationPolicyBuilder()
                                    .RequireAuthenticatedUser()
                                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            #endregion


            // Add the custom services
            //services.AddScoped<IProductRepository, SQLProductRepository>();
            services.AddTransient<IUnitOfWorkRepository, UnitOfWorkRepository>();

            // Add Xente Payment class
            services.AddSingleton<XentePayment>(provider => new XentePayment(Configuration["XenteConnection:ApiKey"], Configuration["XenteConnection:Password"], Mode.Sandbox));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            //Cross origin resource sharing to allow only our application to access
            app.UseCors(
                options => options
                                   .AllowAnyOrigin()
                                   .AllowAnyMethod()
                                   .AllowAnyHeader()
                );

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("The shopping Application is running!");
            });
        }
    }
}
