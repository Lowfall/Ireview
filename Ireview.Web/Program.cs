using Ireview.Infrastructure.Contexts;
using Microsoft.AspNetCore.Identity;
using Ireview.Infrastructure.Identity.Models;
using Ireview.Web.Mapping;
using Microsoft.EntityFrameworkCore;
using Ireview.Core.Mapping;
using AutoMapper;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using System.Configuration;
using CloudinaryDotNet;
using Ireview.Web.Configuration;
using Ireview.Web.Interfaces;
using System.Drawing;
using Ireview.Web.Services;
using Microsoft.AspNetCore.HttpOverrides;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString,new MySqlServerVersion(new Version(8,0,33))));
builder.Services.AddLocalization(opt => opt.ResourcesPath = "Resources");
builder.Services.AddRazorPages().AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();
builder.Services.AddControllersWithViews().AddViewLocalization();
builder.Services.AddAutoMapper(configuration =>
{
    configuration.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    configuration.AddProfile(new AssemblyMappingProfile(Assembly.GetAssembly(typeof(IMapTo<>))!));
});
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme; 
}).AddGoogle(opt =>
{
    opt.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    opt.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
}).AddVkontakte(opt =>
{
    opt.ClientId = builder.Configuration["Authentication:VK:ClientId"];
    opt.ClientSecret = builder.Configuration["Authentication:VK:ClientSecret"];
});
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddScoped<IImageService, ImageService>();


builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddRazorPages();
var app = builder.Build();

var cultures = new List<CultureInfo> {
    new CultureInfo("en"),
    new CultureInfo("ru")
};

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRequestLocalization(options =>
{
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("ru");
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedProto
});

app.UseDeveloperExceptionPage();
app.UseDatabaseErrorPage();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
