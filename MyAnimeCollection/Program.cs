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

app.MapControllerRoute(
    name: "user_list_details",
    pattern: "user/{id}/{id_lista}",
    defaults: new { controller = "UserList", action = "Details" });

app.MapControllerRoute(
    name: "userSearch",
    pattern: "search/user",
    defaults: new { controller = "User", action = "Search" });
    
app.MapControllerRoute(
    name: "animeSearch",
    pattern: "search/anime",
    defaults: new { controller = "Anime", action = "Search" });

app.MapControllerRoute(
    name: "userProfile",
    pattern: "user/{id}",
    defaults: new { controller = "User", action = "Profile" });

app.Run();
app.Run();