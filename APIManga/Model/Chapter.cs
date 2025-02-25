using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIManga.Model
{
	public class Chapter
	{
		[Key]
		public int Id { get; private set; }
		public int MangaId { get; set; }
		public Manga Manga { get; set; }
		public string? Title { get; set; }
		public string Number { get; set; }
		public DateTime? ReleaseDate { get; set; }
		public virtual List<Page> Pages { get; set; }
	}
}