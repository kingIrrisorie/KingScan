using System.ComponentModel.DataAnnotations;

namespace APIManga.Model
{
	public class Image
	{
		public int Id { get; set; }
		public int PageId { get; set; }
		public string ImagePath { get; set; }
		public int Order { get; set; }
		public Page Page { get; set; }
	}
}
