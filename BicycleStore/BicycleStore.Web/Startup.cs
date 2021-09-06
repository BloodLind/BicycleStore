using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.EntityFrameworkCore;
using BicycleStore.BikesDatabase.Context;
using BicycleStore.Identity.Contexts;
using BicycleStore.Identity.Models;
using Microsoft.AspNetCore.Identity;
using BicycleStore.Identity.Repositories;
using BicycleStore.Core.Infrastructure.Interfaces;
using BicycleStore.BikesDatabase.Models;
using BicycleStore.BikesDatabase.Repositories;
using Microsoft.AspNetCore.Mvc;
using BicycleStore.Web.Services.Interfaces;
using BicycleStore.Web.Services;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BicycleStore.Web
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
          
            services.AddControllersWithViews();
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });
           
            services.AddDbContext<BicycleContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();
            services.AddDbContext<IdentityUsersContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));
            services.AddIdentity<User, Role>(options => options.Stores.MaxLengthForKeys = 128)
              .AddEntityFrameworkStores<IdentityUsersContext>()
             .AddDefaultTokenProviders();
            services.AddDistributedMemoryCache();
            services.AddSession();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-mega-puper-key"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                    opt =>
                    {
                        opt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = key,
                            ValidateAudience = false,
                            ValidateIssuer = false,
                        };

                    });


            IdentityBuilder identityBuilder = services.AddIdentityCore<User>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.AddTransient<IdentityUsersContext, IdentityUsersContext>();
            services.AddTransient<DbContext, BicycleContext>();
            services.AddTransient<IRepository<Bicycle>, BicycleRepository>();
            services.AddTransient<IRepository<Order>, OrderRepository>();
            services.AddTransient<IJwtGenerator, JwtGenerator>();

            identityBuilder.AddRoles<Role>();
            identityBuilder.AddUserManager<UserManager<User>>();
            identityBuilder.AddRoleManager<RoleManager<Role>>();

            services.AddTransient<RoleRepository, RoleRepository>();
            services.AddTransient<UserRepository, UserRepository>();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/Login";
               
            }
            );
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseRouting();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthorization();

            
            app.UseMvc(route =>
            {
                route.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }
    }
}
