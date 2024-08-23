/*using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace frontend.Models
{
	public class Chapter
	{
		[Key]
		public int Id { get; private set; }

		[ForeignKey("Manga")]
		public int MangaId { get; set; }
		public Manga Manga { get; set; }


		public string? Title { get; set; }

		public int Number {  get; set; }

		public ICollection<Page> Pages { get; set; }
	}
}
*/