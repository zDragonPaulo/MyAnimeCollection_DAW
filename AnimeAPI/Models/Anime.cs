using System.Collections.Generic;

namespace AnimeAPI.Models
{
    public class Anime
    {
        public int AnimeId { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public string? ImageURL { get; set; }
        public int NumberEpisodes { get; set; }

        // Agora os géneros serão uma lista de strings, representando o nome do género.
        public List<string> Genres { get; set; } = new List<string>();
    }
}
