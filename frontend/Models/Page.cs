/*using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace frontend.Models
{
	public class Page
	{
		[Key]
		public int Id { get; set; }
		[ForeignKey("Chapter")]
		public int ChapterId { get; set; }
		public Chapter Chapter { get; set; }
		public int? PageNumber { get; set; }
		public virtual ICollection<Image> Images { get; set; }
	}
}
*/