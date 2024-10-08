﻿using Microsoft.EntityFrameworkCore;
using APIManga.Model;
using Humanizer.Localisation;

namespace APIManga.Context
{
	public class KingIrrisorieScanContext : DbContext
	{
		public KingIrrisorieScanContext(DbContextOptions<KingIrrisorieScanContext> options) : base(options) { }
		
		public DbSet<Manga> Mangas { get; set; }
		public DbSet<Author> Authors { get; set; }
		public DbSet<Gender> Genres { get; set; }
		//public DbSet<Chapter> Chapters { get; set; }
		//public DbSet<Page> Pages { get; set; }
		//public DbSet<Image> Images { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Author>()
			.HasMany(a => a.Mangas)
			.WithOne(m => m.Author)
			.HasForeignKey(m => m.AuthorId);

            modelBuilder.Entity<Manga>()
            .HasMany(m => m.Genres)
            .WithMany(g => g.Mangas)
            .UsingEntity<Dictionary<string, object>>(
            "MangaGenre",
            j => j.HasOne<Gender>().WithMany().HasForeignKey("GenreId"),
            j => j.HasOne<Manga>().WithMany().HasForeignKey("MangaId"));

            //modelBuilder.Entity<Manga>()
            //.HasMany(m => m.Chapters)
            //.WithOne(c => c.Manga)
            //.HasForeignKey(c => c.MangaId);



            //modelBuilder.Entity<Chapter>()
            //.HasMany(c => c.Pages)
            //.WithOne(p => p.Chapter)
            //.HasForeignKey(p => p.ChapterId);

            //modelBuilder.Entity<Page>()
            //.HasMany(p => p.Images)
            //.WithOne(i => i.Page)
            //	.HasForeignKey(i => i.PageId);
        }
    }
}
