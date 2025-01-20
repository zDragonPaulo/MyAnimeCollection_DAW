using Microsoft.EntityFrameworkCore;
using AnimeAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Sets the DataDirectory to the application's base directory
AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(Directory.GetCurrentDirectory(), "APP_Data"));
Console.WriteLine($"DataDirectory: {AppDomain.CurrentDomain.GetData("DataDirectory")}");

// Adds database services to the DI container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var resolvedConnectionString = connectionString.Replace("|DataDirectory|", AppDomain.CurrentDomain.GetData("DataDirectory")?.ToString() ?? string.Empty);
    options.UseSqlServer(resolvedConnectionString);
});

// Adds the JinkanApiService to the DI container
builder.Services.AddHttpClient<JinkanApiService>();

// Adds the AnimeService to the DI container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adds the CORS policy
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

// Enable CORS
app.UseCors();

// Middleware for HTTPS redirection
app.UseHttpsRedirection();

// Middleware for routing
app.MapControllers();

// Calls the seeding method
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var apiService = services.GetRequiredService<JinkanApiService>();
    await AnimeSeeder.SeedDataAsync(context, apiService); 
}

// Configures the Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Executes the application
app.Run();
