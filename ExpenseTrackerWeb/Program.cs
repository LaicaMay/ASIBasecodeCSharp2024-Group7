using ExpenseTracker;
using ExpenseTracker.Data.Data;
using ExpenseTracker.Resources;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ExpenseTrackerDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme
    ).AddCookie(option => {
        option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });


// Register your custom authorization handler
builder.Services.AddSingleton<IAuthorizationHandler, RolesInDBAuthorizationHandler>();

// Add authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAuthenticatedUser", policy =>
        policy.RequireAuthenticatedUser()); // This policy ensures that users are authenticated
});

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

app.UseAuthentication(); // Ensure authentication middleware is used
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=ExpenseTracker}/{id?}");

app.Run();

