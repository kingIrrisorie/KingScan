namespace APIManga.Model
{
	public class Page
	{
		public int Id { get; set; }
		public int ChapterId { get; set; }
		public Chapter Chapter { get; set; }
		public int? PageNumber { get; set; }
		public virtual ICollection<Image> Images { get; set; }
	}
}
