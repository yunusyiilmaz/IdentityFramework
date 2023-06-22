using Autofac.Core;
using DataAccess.Context.Concrete;
using DataAccess.Repository.Abstract;
using DataAccess.Repository.Concrete;
using Entity;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project_Exam.Web.Api;
using Project_Exam.Web.CustomValidator;
using Project_Exam.Web.Helper;
using Project_Exam.Web.Hubs;
using SendGrid;
using Service.Abstract;
using Service.Concrete;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
//builder.Services.Configure<SecurityStampValidatorOptions>(options =>
//{
//    options.ValidationInterval = TimeSpan.FromMinutes(0);
//});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAuthorization();
builder.Services.AddAuthentication();


builder.Services.AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = true).AddPasswordValidator<CustomPasswordValidator>().AddUserValidator<CustomUserValidator>().AddErrorDescriber<CustomIdentityErrorDescriber>()
    .AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();
builder.Services.AddRazorPages();

builder.Services
    .AddSignalR()
    .AddHubOptions<MyHub>(options =>
    {
        options.EnableDetailedErrors = true;
    });
//builder.Services.AddAuthentication().AddFacebook(opts =>
//{
//    opts.AppId = configuration["Authentication:Facebook:AppId"];
//    opts.AppSecret = configuration["Authentication:Facebook:AppSecret"];
//}).AddGoogle(opts =>
//{
//    opts.ClientId = configuration["Authentication:Google:ClientID"];

//    opts.ClientSecret = configuration["Authentication:Google:ClientSecret"];
//});
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefgðhýijklmnöopqrsþtuüvwxyzABCDEFGÐHIÝJKLMNOÖPQRSÞTUÜVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
});

builder.Services.ConfigureApplicationCookie(opts =>
{
    opts.LoginPath = new PathString("/Login/SignIn");
    opts.LogoutPath = new PathString("/Login/SignIn");
    opts.Cookie = new CookieBuilder
    {
        Name = "Project_Exam",
        HttpOnly = true,
        SameSite = SameSiteMode.Lax,
        SecurePolicy = CookieSecurePolicy.SameAsRequest
    };
    opts.SlidingExpiration = true;
    opts.ExpireTimeSpan = System.TimeSpan.FromDays(60);
    opts.AccessDeniedPath = new PathString("/Member/AccsessDenied");
});


builder.Services.AddTransient<IMailService, SendGridMailService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<MyHub>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{

    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapHub<MyHub>("/myhub");

app.UseStatusCodePagesWithRedirects("/ErrorPage/{0}");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
