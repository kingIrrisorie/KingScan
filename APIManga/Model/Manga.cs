using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace APIManga.Model
{
	public enum StatusManga
	{
		EmProgresso,
		Finalizado,
		Abandonado
	}
	public class Manga
	{
		[Key]
		public int Id { get; private set; }

		[Required]
		public string Title { get; set; }

		[Column(TypeName = "nvarchar(20)")]
		public StatusManga Status { get; set; }

		public string? Description { get; set; }

		[DataType(DataType.Date)]
		public DateTime? Released {  get; set; }

		public ICollection<Chapter> Chapters { get; set; }
	}
}
