using Microsoft.EntityFrameworkCore;
using AnimeAPI.Data;
using AnimeAPI.Models;

public static class AnimeSeeder
{
    public static async Task SeedDataAsync(ApplicationDbContext context, JinkanApiService apiService)
    {
        if (!context.Animes.Any()) // Verifica se já existem animes na base de dados
        {
            var animes = await apiService.GetAnimesAsync(); // Obtém os animes da API
            context.Animes.AddRange(animes); // Adiciona os animes à base de dados
            await context.SaveChangesAsync(); // Salva as alterações no banco de dados
        }
    }
}
