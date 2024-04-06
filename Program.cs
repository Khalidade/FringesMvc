using DevsTutorialCenterMVC.Services;
using FringesMVC;
using FringesMVC.Hubs;
using FringesMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddScoped<BaseService>();

services.AddDbContext<FringeAppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<FringeAppDbContext>()
    .AddDefaultTokenProviders();

services.AddHttpClient("apiClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:7173");
});

services.AddControllersWithViews();
services.AddSignalR();

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

app.UseAuthentication(); // Add this line to enable authentication
app.UseAuthorization(); // Add this line to enable authorization

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");
app.MapHub<ChatHub>("/chatHub");

app.Run();
