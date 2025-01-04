using Microsoft.EntityFrameworkCore;
using AnimeAPI.Data;
using AnimeAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Set the DataDirectory to the application's base directory
AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(Directory.GetCurrentDirectory(), "APP_Data"));


// Configuração do DbContext com SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar o serviço da API Jikan
builder.Services.AddHttpClient<JinkanApiService>();

// Adicionar serviços essenciais
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adicionar CORS, se necessário (opcional)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Habilitar CORS (se configurado)
app.UseCors();

// Middleware de redirecionamento para HTTPS
app.UseHttpsRedirection();

// Middleware de roteamento de controladores
app.MapControllers();

// Chamar o Seeder durante a inicialização
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var apiService = services.GetRequiredService<JinkanApiService>();
    await AnimeSeeder.SeedDataAsync(context, apiService);  // Chama o método de seeding
}

// Configuração do Swagger no ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Executa o aplicativo
app.Run();
