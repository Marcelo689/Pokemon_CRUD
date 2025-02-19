using DB.Data;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebMVC.Data;
using WebMVC.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PokemonContext>(options =>
    options.UseSqlServer(
        connectionString,
            b => b.MigrationsAssembly("WebMVC")
    ));
builder.Services.AddScoped<PokeApiService>();
builder.Services.AddScoped<TreinadorService>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddHttpClient<PokeApiService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.WithOrigins("https://localhost:7199")
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

var testCulture = new CultureInfo("en-US");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(testCulture),
    SupportedCultures = new List<CultureInfo> { testCulture },
    SupportedUICultures = new List<CultureInfo> { testCulture }
};

app.UseRequestLocalization(localizationOptions);

app.UseCors("AllowAllOrigins");
DbInitializer.CreateDbIfNotExists(app);

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=PokemonApi}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
