using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIManga.Model
{
	public class Image
	{
		[Key]
		public int Id { get; set; }
		public int PageId { get; set; }
		public Page Page { get; set; }
		public string ImageUrl { get; set; }
		public int ImageOrder { get; set; }
	}
}