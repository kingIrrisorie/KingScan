using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace frontend.Models
{
	public class Image
	{
		[Key]
		public int Id { get; set; }
		[ForeignKey("Page")]
		public int PageId { get; set; }
		public string ImagePath { get; set; }
		public int Order { get; set; }
		public Page Page { get; set; }
	}
}
