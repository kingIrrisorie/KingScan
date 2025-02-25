using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace APIManga.Model
{
	public class Manga
	{
		[Key]
		public int Id { get; private set; }

		[Required]
		public string Title { get; set; }

		[ForeignKey("Status")]
		public int StatusId { get; set; }
		public Status Status { get; set; }

		[StringLength(500)]
		public string? Description { get; set; }

		[DataType(DataType.Date)]
		[Column(TypeName = "date")]
		public DateTime? ReleaseDate { get; set; }

		public string? ThumbnailUrl { get; set; }
		public virtual List<Author> Authors { get; set; }
		public virtual List<Genre>? Genres { get; set; }
		public virtual List<Chapter>? Chapters { get; set; }
	}
}


