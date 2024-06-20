// using Database;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using GigaKino.Services;
using GigaKino.Models;
using GigaKino.ServicesInterfaces;
using GigaKino;
using System.Runtime.InteropServices;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

System.Environment.SetEnvironmentVariable("WEBSITE_LOAD_USER_PROFILE", "1", EnvironmentVariableTarget.Process);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddScoped<IKinoService, KinoService>();
builder.Services.AddScoped<ISalaService, SalaService>();
builder.Services.AddScoped<IMiejsceService, MiejsceService>();
builder.Services.AddScoped<IFilmService, FilmService>();
builder.Services.AddScoped<ISeansService, SeansService>();
builder.Services.AddScoped<IBiletService, BiletService>();
builder.Services.AddScoped<ITransakcjaService, TransakcjaService>();
builder.Services.AddScoped<IKlientService, KlientService>();
builder.Services.AddScoped<IKontoService, KontoService>();
builder.Services.AddScoped<IPracownikService, PracownikService>();
builder.Services.AddScoped<IRepertuarService, RepertuarService>();

string? connectionString;
if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
        connectionString = builder.Configuration.GetConnectionString("DefaultConnectionWindows");
    }
    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    {
        connectionString = builder.Configuration.GetConnectionString("DefaultConnectionUnix");
    }
    else
    {
        throw new PlatformNotSupportedException("Unsupported OS platform");
}

builder.Services.AddDbContext<KinoContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


var app = builder.Build();
Console.WriteLine("app built");

using (var scope = app.Services.CreateScope())
{
    Console.WriteLine("geting dbContext");
    var dbContext = scope.ServiceProvider.GetRequiredService<KinoContext>();

    try
    {
        dbContext.Database.OpenConnection();
        dbContext.Database.CloseConnection();
        Console.WriteLine("Database connection successful.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
    }
}


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

Console.WriteLine("running the app...");
app.Run();
