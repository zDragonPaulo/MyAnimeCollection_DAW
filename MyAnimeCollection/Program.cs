var builder = WebApplication.CreateBuilder(args);

// Adicionar o serviço AnimeApiService
builder.Services.AddHttpClient<AnimeApiService>();

// Outros serviços
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuração do middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();