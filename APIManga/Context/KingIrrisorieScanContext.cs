using APIManga.Model;
using Microsoft.EntityFrameworkCore;

namespace APIManga.Context
{
	public class KingIrrisorieScanContext : DbContext
	{
		public KingIrrisorieScanContext(DbContextOptions<KingIrrisorieScanContext> options)
			: base(options)
		{
		}
		public DbSet<Manga> Mangas { get; set; }
		public DbSet<Author> Authors { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Status> Statuses { get; set; }
		public DbSet<Chapter> Chapters { get; set; }
		public DbSet<Page> Pages { get; set; }
		public DbSet<Image> Images { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Manga>()
				.HasMany(m => m.Authors)
				.WithMany(a => a.Mangas)
				.UsingEntity(j => j.ToTable("MangaAuthor"));

			builder.Entity<Manga>()
				.HasMany(m => m.Genres)
				.WithMany(g => g.Mangas)
				.UsingEntity(j => j.ToTable("MangaGenre"));

			builder.Entity<Chapter>()
				.HasIndex(c => new { c.MangaId, c.Number })
				.IsUnique();

			builder.Entity<Page>()
				.HasIndex(p => new { p.ChapterId, p.PageNumber })
				.IsUnique();

			builder.Entity<Image>()
				.HasIndex(i => new { i.PageId, i.ImageOrder })
				.IsUnique();
		}
	}
}