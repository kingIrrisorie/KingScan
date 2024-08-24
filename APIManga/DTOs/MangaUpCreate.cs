using APIManga.Model;

namespace APIManga.DTOs
{
    public class MangaUpCreate
    {
        public string Title { get; set; }
        public StatusManga Status { get; set; }
        public string? Description { get; set; }
        public DateTime? Released { get; set; }
        public string? ThumbnailURL { get; set; }
        public string? AuthorName { get; set; }
        public List<string>? GenreNames { get; set; }
    }
}
