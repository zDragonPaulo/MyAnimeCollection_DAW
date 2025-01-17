using Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(Directory.GetCurrentDirectory(), "APP_Data"));
Console.WriteLine($"DataDirectory: {AppDomain.CurrentDomain.GetData("DataDirectory")}");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var resolvedConnectionString = connectionString.Replace("|DataDirectory|", AppDomain.CurrentDomain.GetData("DataDirectory")?.ToString() ?? string.Empty);
    options.UseSqlServer(resolvedConnectionString);
});
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/User/Login"; // Caminho para a página de login
        options.LogoutPath = "/User/Logout"; // Caminho para a página de logout
    });
builder.Services.AddAuthorization();

// Adicionar o serviço AnimeApiService
builder.Services.AddHttpClient<AnimeApiService>();

// Outros serviços
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuração do middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();