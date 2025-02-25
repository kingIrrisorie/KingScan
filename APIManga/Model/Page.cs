using APIManga.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIManga.Model
{
	public class Page
	{
		[Key]
		public int Id { get; set; }
		public int ChapterId { get; set; }
		public Chapter Chapter { get; set; }
		public int? PageNumber { get; set; }
		public virtual List<Image> Images { get; set; }
	}
}