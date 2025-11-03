using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewStore.Application.Interfaces.Contexts;
using NewStore.Application.Interfaces.FacadPatterns;
using NewStore.Application.Services.Products.FacadPattern;
using NewStore.Application.Services.Users.Commands.ChangeStatusUser;
using NewStore.Application.Services.Users.Commands.EditUser;
using NewStore.Application.Services.Users.Commands.LoginUser;
using NewStore.Application.Services.Users.Commands.RegisterUser;
using NewStore.Application.Services.Users.Commands.RemoveUser;
using NewStore.Application.Services.Users.Queris.GetRole;
using NewStore.Application.Services.Users.Queris.GetUser;
using NewStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site
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

            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = new PathString("/");
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
            });

            string connectionString = "Data source=ABALFAZLPC\\MYSQL; Initial Catalog = StoreDb; Integrated Security = True;";
            services.AddControllersWithViews();
            services.AddEntityFrameworkSqlServer().AddDbContext<DataBaseContext>(option => option.UseSqlServer(connectionString));
            services.AddScoped<IDataBaseContext,DataBaseContext>();
            services.AddScoped<IGetUserServise, GetUserService>();
            services.AddScoped<IGetRolesService, GetRolesService>();
            services.AddScoped<IRegisterUserService, RegisterUserService>();
            services.AddScoped<IRemoveUserService, RemoveUserService>();
            services.AddScoped<IChangeUserStatusService, ChangeUserStatusService>();
            services.AddScoped<IEditUserService, EditUserService>();
            services.AddScoped<ILoginUserService, LoginUserService>();

            services.AddScoped<IProductFacad,ProductFacad>();

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

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                // ۱. روت برای Areas (اگر دارید)
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                // ۲. روت پیش‌فرض — همه چیز را پوشش می‌دهد
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapAreaControllerRoute(
            //        name: "AdminArea",
            //        areaName: "Admin",
            //        pattern: "admin/{controller=Users}/{action=Index}/{id?}");

            //    endpoints.MapControllerRoute(
            //        name: "areas",
            //        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            //    // اگر کسی به "/" برود، به /admin/users/create هدایتش کن
            //    endpoints.MapGet("/", context =>
            //    {
            //        context.Response.Redirect("/admin/users/index");
            //        return Task.CompletedTask;
            //    });
            //});


        }
    }
}
