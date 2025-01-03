namespace MyAnimeCollection.Models
{
    public class Anime
    {
        public int MalId { get; set; }
        public string Title { get; set; }
        public double Score { get; set; }
        public Images Images { get; set; }
    }

    public class Images
    {
        public Jpg Jpg { get; set; }
    }

    public class Jpg
    {
        public string ImageUrl { get; set; }
    }

    public class AnimeLists
    {
        public List<Anime> PorVer { get; set; } = new();
        public List<Anime> AVer { get; set; } = new();
        public List<Anime> Completado { get; set; } = new();
    }
}
