using APIManga.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APIManga.DTOs
{
	public class MangaDTO
	{
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		public string Status { get; set; }
		[StringLength(500)]
		public string? Description { get; set; }
		[DataType(DataType.Date)]
		[Column(TypeName = "date")]
		public DateTime? ReleaseDate { get; set; }
		public string? ThumbnailUrl { get; set; }
		public List<string> AuthorNames { get; set; }
		public List<string>? GenreNames { get; set; }
	}
}
