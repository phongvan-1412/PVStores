using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.ModelPattern;
using WebApplication1.Models.entities;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
IConfiguration configuration = config.Build();
string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

builder.Services.AddDbContext<PVStoresContext>(options =>
    options.UseSqlServer(constring, options =>
            builder.Configuration.GetConnectionString("DefaultConnection")
    ));


builder.Services.AddMvc();
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

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapAreaControllerRoute(
        "Admin",
        "Admin",
        "Admin/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
         name: "default",
         pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
