using CryptoTradingPlatform.Core.Contracts;
using CryptoTradingPlatform.Core.Services;
using CryptoTradingPlatform.Data.Models;
using CryptoTradingPlatform.Infrastructure.Data;
using CryptoTradingPlatform.ModelBinders;
using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatfrom.Core.Services;
using CryptoTradingPlatfrom.Infrastructure.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<ICryptoApiService, CryptoApiService>();
builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<ITradingService, TradingService>();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    //use the following for testing
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;

})
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews()
    .AddMvcOptions(options =>
    {
        options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
        options.ModelBinderProviders.Insert(1, new DateTimeModelBinderProvider(FormatingConstants.MyDateFormat));
        options.ModelBinderProviders.Insert(2, new DoubleModelBinderProvider());
        options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
    });

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


//try configure cors
app.UseCors(options =>
{
    options.WithOrigins("https://pro-api.coinmarketcap.com");
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "AssetDetails",
    pattern: "{controller=Assets}/{action=Details}/{assetName}");
app.MapRazorPages();

app.Run();

//my todos
//change user card to view component maybe
//make crypto tables ajax maybe
//add toaster errors for trading
//add user roles 
//write tests
//save asset prices in assetPrice and maybe make graph
//get transaction history between dates 