using RecrutaPlus.Domain.Interfaces;
//using RecrutaPlus.Infra.Data.Context;
//using RecrutaPlus.Infra.Data.Identity.Context;
//using RecrutaPlus.Infra.Data.Identity.Models;
//using RecrutaPlus.Infra.Data.IoC;
using RecrutaPlus.Web;
using RecrutaPlus.Web.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
//using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecrutaPlus.Domain.Interfaces;
using System;


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//ConfigurationManager configuration = builder.Configuration;
//IWebHostEnvironment environment = builder.Environment;

////Default
//var dbProviderFactoryType = configuration.GetSection(AppSettingsWebConst.APPSETTINGS)
//    .GetValue<string>(AppSettingsWebConst.DBPROVIDERFACTORY_TYPE);

//var dbProviderFactoryName = configuration.GetSection(AppSettingsWebConst.APPSETTINGS)
//    .GetValue<string>(AppSettingsWebConst.DBPROVIDERFACTORY_NAME);

//var connectionStringDefault = configuration.GetConnectionString(
// configuration.GetSection(AppSettingsWebConst.APPSETTINGS)
// .GetValue<string>(AppSettingsWebConst.CONNECTIONSTRING_DEFAULT));

////DbContext
//if (dbProviderFactoryType == AppSettingsWebConst.CONNECTIONSTRINGTYPE_MSSQL)
//{
//    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionStringDefault).UseLoggerFactory(AppDbContext.LoggerFactoryDatabase));
//    builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(connectionStringDefault));
//}

//if (dbProviderFactoryType == AppSettingsWebConst.CONNECTIONSTRINGTYPE_MYSQL)
//{
//    builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionStringDefault, ServerVersion.AutoDetect(connectionStringDefault)).UseLoggerFactory(AppDbContext.LoggerFactoryDatabase));

//    builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseMySql(connectionStringDefault, ServerVersion.AutoDetect(connectionStringDefault)));
//}


//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddIdentity<AppIdentityUser, AppIdentityRole>(options =>
//{
//    // Default SignIn settings.
//    options.SignIn.RequireConfirmedAccount = true;
//    options.SignIn.RequireConfirmedEmail = true;
//    options.SignIn.RequireConfirmedPhoneNumber = false;

//    // Default Password settings.
//    options.Password.RequireDigit = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireNonAlphanumeric = false; //Default = true
//    options.Password.RequireUppercase = true;
//    options.Password.RequiredLength = 6;
//    options.Password.RequiredUniqueChars = 1;

//    // Default Lockout settings.
//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
//    options.Lockout.MaxFailedAccessAttempts = 3;
//    options.Lockout.AllowedForNewUsers = true;

//}).AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();

////ClaimsPrincipalFactory
//builder.Services.AddTransient<IUserClaimsPrincipalFactory<AppIdentityUser>, AppIdentityUserClaimsPrincipalFactory>();

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.AccessDeniedPath = "/Auth/Account/AccessDenied";
//    options.Cookie.Name = "MakeRDV";
//    options.Cookie.HttpOnly = true;
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
//    options.LoginPath = "/Auth/Account/Login";
//    options.LogoutPath = "/Auth/Account/Logout";

//    // ReturnUrlParameter requires 
//    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
//    options.SlidingExpiration = true;

//    options.SessionStore = new MemoryCacheTicketStore();
//});

////DataProtection
//builder.Services.AddDataProtection().PersistKeysToDbContext<AppIdentityDbContext>();

//// Configure your policies
//AuthorizationConfig.RegisterServices(builder.Services);

//// Add application builder.Services.

////DI
//NativeInjectorConfig.RegisterServices(builder.Services);

////Email
//builder.Services.AddTransient<IEmailSender, EmailSender>();
////builder.Services.AddTransient<IEmailSenderAsync, EmailSenderAsync>();

////AutoMapper
//builder.Services.AddAutoMapperSetup();

////Logging
////builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

//builder.Services.AddResponseCaching();
//builder.Services.AddResponseCompression(options =>
//{
//    options.Providers.Add<BrotliCompressionProvider>();
//    options.Providers.Add<GzipCompressionProvider>();
//});

//builder.Services.AddMemoryCache();

//builder.Services.AddControllersWithViews().AddNewtonsoftJson().AddRazorRuntimeCompilation();

//builder.Services.AddRazorPages().AddRazorRuntimeCompilation();



////app
//var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//    app.UseMigrationsEndPoint();
//}
//else
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}
//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();

//app.UseResponseCaching();
//app.UseResponseCompression();

////Auth
//app.MapControllerRoute(
//    name: "Auth",
//    pattern: "{area:exists}/{controller=Account}/{action=Login}/{id?}");

////BackOffice
//app.MapControllerRoute(
//    name: "BackOffice",
//    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

////default 
///*app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{rdvid?}/{rdvlancamentoid?}");*/

////default
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");


//app.Run();


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();