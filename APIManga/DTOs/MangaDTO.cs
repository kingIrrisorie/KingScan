using APIManga.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APIManga.DTOs
{
	public class MangaDTO
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public StatusManga Status { get; set; }
		public string? Description { get; set; }
		public DateTime? Released { get; set; }
		public string? ThumbnailURL { get; set; }
		public string? AuthorName { get; set; }
		public List<string>? GenreNames { get; set; }
	}
}
