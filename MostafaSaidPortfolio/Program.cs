using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MostafaSaidPortfolio.Data;
using MostafaSaidPortfolio.Domain.Entities;
using MostafaSaidPortfolio.Extensions;
using System.Globalization;
using System.Threading.RateLimiting;

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
    // Strong password policy
    options.Password.RequireDigit           = true;
    options.Password.RequireUppercase       = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength         = 10;
    options.Password.RequiredUniqueChars    = 4;

    // Account lockout after 5 failed attempts for 15 minutes
    options.Lockout.DefaultLockoutTimeSpan  = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers      = true;

    // Unique email required
    options.User.RequireUniqueEmail         = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Configure secure cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly    = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite    = SameSiteMode.Strict;
    options.LoginPath          = "/Account/Login";
    options.AccessDeniedPath   = "/Account/AccessDenied";
    options.ExpireTimeSpan     = TimeSpan.FromHours(8);
    options.SlidingExpiration  = true;
});

// Rate limiting — fixed window, keyed by client IP
builder.Services.AddRateLimiter(opts =>
{
    opts.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    opts.AddFixedWindowLimiter("contact", policy =>
    {
        policy.Window       = TimeSpan.FromHours(1);
        policy.PermitLimit  = 5;
        policy.QueueLimit   = 0;
        policy.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });

    opts.AddFixedWindowLimiter("newsletter", policy =>
    {
        policy.Window       = TimeSpan.FromHours(1);
        policy.PermitLimit  = 3;
        policy.QueueLimit   = 0;
        policy.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });

    opts.AddFixedWindowLimiter("login", policy =>
    {
        policy.Window       = TimeSpan.FromMinutes(15);
        policy.PermitLimit  = 10;
        policy.QueueLimit   = 0;
        policy.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});

// Dapper connection factory (singleton)
builder.Services.AddSingleton<DbConnectionFactory>();

// Register app services (Dapper-based via UoW)
builder.Services.AddCustomServices();

// Localization
builder.Services.AddLocalization(opts => opts.ResourcesPath = "");

var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("ar") };
builder.Services.Configure<RequestLocalizationOptions>(opts =>
{
    opts.DefaultRequestCulture = new RequestCulture("en");
    opts.SupportedCultures     = supportedCultures;
    opts.SupportedUICultures   = supportedCultures;
    opts.FallBackToParentCultures   = true;
    opts.FallBackToParentUICultures = true;
    opts.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

var app = builder.Build();

// Seed Identity roles and admin user
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();

    // Seed Admin role
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    if (!await roleManager.RoleExistsAsync("Admin"))
        await roleManager.CreateAsync(new IdentityRole("Admin"));

    var factory = scope.ServiceProvider.GetRequiredService<DbConnectionFactory>();
    await DatabaseInitializer.InitializeAsync(factory);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Security headers middleware
app.Use(async (context, next) =>
{
    var headers = context.Response.Headers;
    headers.Append("X-Content-Type-Options",  "nosniff");
    headers.Append("X-Frame-Options",          "SAMEORIGIN");
    headers.Append("X-XSS-Protection",         "1; mode=block");
    headers.Append("Referrer-Policy",           "strict-origin-when-cross-origin");
    headers.Append("Permissions-Policy",        "camera=(), microphone=(), geolocation=()");

    if (!app.Environment.IsDevelopment())
    {
        headers.Append("Content-Security-Policy",
            "default-src 'self'; " +
            "script-src 'self' 'unsafe-inline' https://cdn.tailwindcss.com https://cdnjs.cloudflare.com; " +
            "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com https://cdn.tailwindcss.com; " +
            "font-src 'self' https://fonts.gstatic.com; " +
            "img-src 'self' data: https:; " +
            "connect-src 'self'");
    }

    await next();
});

app.UseStaticFiles();

app.UseRequestLocalization(
    app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseRateLimiter();

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
