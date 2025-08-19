using Microsoft.EntityFrameworkCore;
using MostafaSaidPortfolio.Data;
using MostafaSaidPortfolio.Extensions;
using MostafaSaidPortfolio.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Add custom services/extensions
builder.Services.AddCustomServices();
builder.Services.AddCustomEmail();
builder.Services.AddCustomLocalization();
builder.Services.AddCustomNewsletter();
builder.Services.AddCustomEvents();

// Add MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
