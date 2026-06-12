using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using MostafaSaidPortfolio.Data;
using MostafaSaidPortfolio.Extensions;

var builder = WebApplication.CreateBuilder(args);

// ── Service Registration ─────────────────────────────────────────────────────
builder.Services.AddCustomDbContext(builder.Configuration);
builder.Services.AddCustomIdentity();
builder.Services.AddCustomRateLimiting();
builder.Services.AddCustomServices();
builder.Services.AddCustomLocalization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

var app = builder.Build();

// ── Database Seeding ─────────────────────────────────────────────────────────
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    if (!await roleManager.RoleExistsAsync("Admin"))
        await roleManager.CreateAsync(new IdentityRole("Admin"));

    var factory = scope.ServiceProvider.GetRequiredService<DbConnectionFactory>();
    await DatabaseInitializer.InitializeAsync(factory);
}

// ── Middleware Pipeline ───────────────────────────────────────────────────────
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSecurityHeaders(app.Environment);
app.UseStaticFiles();
app.UseRequestLocalization(
    app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
app.UseRateLimiter();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// ── Route Mapping ────────────────────────────────────────────────────────────
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
