namespace AnimeAPI.Models
{
    // Anime class and its properties
    public class Anime
    {
        public int AnimeId { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public string? ImageURL { get; set; }
        public int NumberEpisodes { get; set; }
        public List<string> Genres { get; set; } = new List<string>();
    }
}
