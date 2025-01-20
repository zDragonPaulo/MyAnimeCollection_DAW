using Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Sets the DataDirectory to the application's base directory
AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(Directory.GetCurrentDirectory(), "APP_Data"));
Console.WriteLine($"DataDirectory: {AppDomain.CurrentDomain.GetData("DataDirectory")}");

// Adds database services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var resolvedConnectionString = connectionString.Replace("|DataDirectory|", AppDomain.CurrentDomain.GetData("DataDirectory")?.ToString() ?? string.Empty);
    options.UseSqlServer(resolvedConnectionString);
});

// Adds authentication services to the container.
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/User/Login";
        options.LogoutPath = "/User/Logout";
    });
builder.Services.AddAuthorization();

// Adds the required services 
builder.Services.AddHttpClient<AnimeApiService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware configuration
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Route configuration
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

app.MapControllerRoute(
    name: "userLogin",
    pattern: "user/login",
    defaults: new { controller = "User", action = "Login" });

app.MapControllerRoute(
    name: "userLogout",
    pattern: "user/logout",
    defaults: new { controller = "User", action = "Logout" });

app.MapControllerRoute(
    name: "userRegister",
    pattern: "user/register",
    defaults: new { controller = "User", action = "Register" });

// Executes the application
app.Run();