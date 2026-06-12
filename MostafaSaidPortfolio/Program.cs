using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MostafaSaidPortfolio.Data;
using MostafaSaidPortfolio.Extensions;
using MostafaSaidPortfolio.Models;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var rawConnectionString = Environment.GetEnvironmentVariable("DATABASE_URL")
    ?? builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("DATABASE_URL environment variable not set.");

var connectionString = ConnectionHelper.ToNpgsqlConnectionString(rawConnectionString);

// Identity with PostgreSQL via EF Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit           = false;
    options.Password.RequireUppercase       = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength         = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Dapper connection factory (singleton)
builder.Services.AddSingleton<DbConnectionFactory>();

// Register app services (Dapper-based via UoW)
builder.Services.AddCustomServices();

// Localization — resource files live in Resources/
builder.Services.AddLocalization(opts => opts.ResourcesPath = "Resources");

// Supported cultures
var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("ar") };
builder.Services.Configure<RequestLocalizationOptions>(opts =>
{
    opts.DefaultRequestCulture = new RequestCulture("en");
    opts.SupportedCultures     = supportedCultures;
    opts.SupportedUICultures   = supportedCultures;
    opts.FallBackToParentCultures   = true;
    opts.FallBackToParentUICultures = true;

    // Cookie is the primary provider (set by CultureController)
    opts.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

var app = builder.Build();

// Create Identity schema + initialize custom tables + seed data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();

    var factory = scope.ServiceProvider.GetRequiredService<DbConnectionFactory>();
    await DatabaseInitializer.InitializeAsync(factory);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();

// Localization middleware BEFORE routing
app.UseRequestLocalization(
    app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
