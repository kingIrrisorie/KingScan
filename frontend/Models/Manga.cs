using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace frontend.Models
{
    public enum StatusManga
    {
        EmProgresso,
        Finalizado,
        Abandonado
    }
    public class Manga
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public StatusManga Status { get; set; }
        public string? Description { get; set; }
        public DateTime? Released { get; set; }
        public string? ThumbnailURL { get; set; }
        public string? AuthorName { get; set; }
        public List<string>? GenreNames { get; set; }

        [ForeignKey("Author")]
        public int? AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}