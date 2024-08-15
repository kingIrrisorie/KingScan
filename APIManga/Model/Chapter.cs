using System.ComponentModel.DataAnnotations;

namespace APIManga.Model
{
	public class Chapter
	{
		[Key]
		public int Id { get; private set; }

		public int MangId { get; set; }

		public int Number {  get; set; }

		public Manga manga { get; set; }
	}
}
