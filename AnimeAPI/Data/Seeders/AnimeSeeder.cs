using AnimeAPI.Data;

public static class AnimeSeeder
{
    // Populates the database with animes from the Jikan API
    public static async Task SeedDataAsync(ApplicationDbContext context, JinkanApiService apiService)
    {
        if (!context.Animes.Any())
        {
            var animes = await apiService.GetAnimesAsync();
            context.Animes.AddRange(animes);
            await context.SaveChangesAsync();
        }
    }
}
