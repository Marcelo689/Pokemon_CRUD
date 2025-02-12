using DB.Data;
using Microsoft.EntityFrameworkCore;
using WebMVC.Data;
using WebMVC.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
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
app.UseCors("AllowAllOrigins");

// Middleware para lidar com OPTIONS
DbInitializer.CreateDbIfNotExists(app);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
