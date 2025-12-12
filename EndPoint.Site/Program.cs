using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewStore.Application.Interfaces.Contexts;
using NewStore.Application.Interfaces.FacadPatterns;
using NewStore.Application.Services.Common.FacadPattern;
using NewStore.Application.Services.HomePage.FacadPattren;
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
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// -------------------------
// Add Services
// -------------------------

builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
});

string connectionString = "Data Source=ABALFAZLPC\\MYSQL; Initial Catalog=StoreDb; Integrated Security=True; TrustServerCertificate=True;";


builder.Services.AddControllersWithViews();

// EF Core
builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();

// DI Services
builder.Services.AddScoped<IGetUserServise, GetUserService>();
builder.Services.AddScoped<IGetRolesService, GetRolesService>();
builder.Services.AddScoped<IRegisterUserService, RegisterUserService>();
builder.Services.AddScoped<IRemoveUserService, RemoveUserService>();
builder.Services.AddScoped<IChangeUserStatusService, ChangeUserStatusService>();
builder.Services.AddScoped<IEditUserService, EditUserService>();
builder.Services.AddScoped<ILoginUserService, LoginUserService>();
builder.Services.AddScoped<IProductFacad, ProductFacad>();
builder.Services.AddScoped<IProductFacadForAdmin, ProductFacadForAdmin>();
builder.Services.AddScoped<ICommonFacad, CommonFacad>();
builder.Services.AddScoped<IHomePageFacad, HomePageFacad>();

// -------------------------
// Build Application
// -------------------------
var app = builder.Build();

// -------------------------
// Middleware Pipeline
// -------------------------

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ترتیب مهم است:
app.UseAuthentication();
app.UseAuthorization();

// -------------------------
// Endpoints + Areas
// -------------------------
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

//Redirect root → /products/index
//app.MapGet("/", context =>
//{
//    context.Response.Redirect("/admin/HomePage/AddPageImage");
//return Task.CompletedTask;
//});

app.Run();
