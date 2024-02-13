using BlazorMaestroDetalle.UI.Services;
using BlazorMaestroDetalle.UI.DAO;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

// Registra el servicio de base de datos en el contenedor de inyección de dependencias como Singleton
var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");

builder.Services.AddSingleton<MySqlConnection>(_ => new MySqlConnection(connectionString));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<LibroService>();
builder.Services.AddSingleton<LibroDAO>();
builder.Services.AddSingleton<SocioDAO>();
builder.Services.AddSingleton<SocioService>();
builder.Services.AddSingleton<PrestamoService>();
builder.Services.AddSingleton<PrestamoDAO>();


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
