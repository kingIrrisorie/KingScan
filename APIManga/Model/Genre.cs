using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIManga.Model
{
	public class Genre
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual List<Manga> Mangas { get; set; }
	}
}