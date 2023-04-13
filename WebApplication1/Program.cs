using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.ModelPattern;
using WebApplication1.Models.entities;
using Microsoft.AspNetCore.Mvc.Razor;
using Stripe;
using WebApplication1.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Sessions
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});

//Connect DB
var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    IConfiguration configuration = config.Build();
    string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

    builder.Services.AddDbContext<PVStoresContext>(options =>
        options.UseSqlServer(constring, options =>
                builder.Configuration.GetConnectionString("DefaultConnection")
        ));


builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddFacebook(FacebookDefaults.AuthenticationScheme, options =>
{
    options.AppId = builder.Configuration["Authentication:Facebook:AppId"];
    options.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];

})
.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
})
.AddCookie(options =>
{
    options.Cookie.Name = "Cookies";
});

builder.Services.AddMvc();
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
var app = builder.Build();

//Integrate Stripe
StripeConfiguration.SetApiKey(configuration.GetSection("Stripe")["Secretkey"]);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

//Routes Pattern
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
