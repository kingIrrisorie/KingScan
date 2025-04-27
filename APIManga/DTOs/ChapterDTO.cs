namespace APIManga.DTOs
{
	public class ChapterDTO
	{
		public int Id { get; set; }
		public int MangaId { get; set; }
		public string Title { get; set; }
		public string Number { get; set; }
		public DateTime? ReleaseDate { get; set; }
	}
}
