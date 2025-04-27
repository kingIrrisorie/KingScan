using APIManga.Context;
using APIManga.DTOs;
using APIManga.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace APIManga.Services
{
	public class MangaService
	{
		private readonly KingIrrisorieScanContext _context;

		public MangaService(KingIrrisorieScanContext context)
		{
			_context = context;
		}

		public async Task<List<MangaDTO>> GetAllMangasAsync()
		{
			return await _context.Mangas
				.Include(m => m.Status)
				.Include(m => m.Authors)
				.Include(m => m.Genres)
				.Select(m => new MangaDTO
				{
					Id = m.Id,
					Title = m.Title,
					Status = m.Status.Name,
					Description = m.Description,
					ReleaseDate = m.ReleaseDate,
					ThumbnailUrl = m.ThumbnailUrl,
					AuthorNames = m.Authors.Select(a => a.Name).ToList(),
					GenreNames = m.Genres.Select(g => g.Name).ToList()
				})
				.ToListAsync();
		}

		public async Task<MangaDTO> GetMangaByIdAsync(int id)
		{
			var manga = await _context.Mangas
				.Include(m => m.Status)
				.Include(m => m.Authors)
				.Include(m => m.Genres)
				.FirstOrDefaultAsync(m => m.Id == id);

			if (manga == null)
			{
				return null;
			}

			var mangaDTO = new MangaDTO
			{
				Id = manga.Id,
				Title = manga.Title,
				Status = manga.Status.Name,
				Description = manga.Description,
				ReleaseDate = manga.ReleaseDate,
				ThumbnailUrl = manga.ThumbnailUrl,
				AuthorNames = manga.Authors.Select(a => a.Name).ToList(),
				GenreNames = manga.Genres.Select(g => g.Name).ToList()
			};

			return mangaDTO;
		}

		public async Task<Manga> CreateMangaAsync(MangaDTO mangaDTO)
		{
			var manga = new Manga();
			if (string.IsNullOrEmpty(mangaDTO.Title))
			{
				throw new Exception("Title is required");
			}

			manga.Title = mangaDTO.Title;
			manga.Description = mangaDTO.Description;
			manga.ReleaseDate = mangaDTO.ReleaseDate;
			manga.ThumbnailUrl = mangaDTO.ThumbnailUrl;

			var statusName = string.IsNullOrEmpty(mangaDTO.Status) ? "Em progresso" : mangaDTO.Status;
			var status = await _context.Statuses.FirstOrDefaultAsync(s => s.Name == statusName);
			if (status == null)
			{
				status = new Status { Name = statusName };
				_context.Statuses.Add(status);
			}
			manga.Status = status;

			if (mangaDTO.AuthorNames != null && mangaDTO.AuthorNames.Any())
			{
				manga.Authors = new List<Author>();
				foreach (var authorName in mangaDTO.AuthorNames)
				{
					var author = await _context.Authors.FirstOrDefaultAsync(a => a.Name == authorName);
					if (author == null)
					{
						author = new Author { Name = authorName };
						_context.Authors.Add(author);
					}
					manga.Authors.Add(author);
				}
			}

			if (mangaDTO.GenreNames != null && mangaDTO.GenreNames.Any())
			{
				manga.Genres = new List<Genre>();
				foreach (var genreName in mangaDTO.GenreNames)
				{
					var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Name == genreName);
					if (genre == null)
					{
						genre = new Genre { Name = genreName };
						_context.Genres.Add(genre);
					}
					manga.Genres.Add(genre);
				}
			}

			_context.Mangas.Add(manga);
			await _context.SaveChangesAsync();

			return manga;
		}
		public async Task<MangaDTO> UpdateMangaAsync(MangaDTO mangaDTO)
		{
			try
			{
				var manga = await _context.Mangas
					.Include(m => m.Authors)
					.Include(m => m.Genres)
					.FirstOrDefaultAsync(m => m.Id == mangaDTO.Id);

				if (manga == null)
				{
					throw new ArgumentException("Mangá não encontrado.");
				}

				manga.Title = mangaDTO.Title;
				manga.Description = mangaDTO.Description;
				manga.ReleaseDate = mangaDTO.ReleaseDate;
				manga.ThumbnailUrl = mangaDTO.ThumbnailUrl;

				var status = await _context.Statuses.FirstOrDefaultAsync(s => s.Name == mangaDTO.Status);
				if (status == null)
				{
					status = new Status { Name = mangaDTO.Status };
					await _context.Statuses.AddAsync(status);
				}
				manga.Status = status;

				manga.Authors.Clear();
				if (mangaDTO.AuthorNames != null && mangaDTO.AuthorNames.Any())
				{
					var authors = await _context.Authors
						.Where(a => mangaDTO.AuthorNames.Contains(a.Name))
						.ToListAsync();

					var newAuthorNames = mangaDTO.AuthorNames.Except(authors.Select(a => a.Name)).ToList();
					foreach (var newAuthorName in newAuthorNames)
					{
						var newAuthor = new Author { Name = newAuthorName };
						await _context.Authors.AddAsync(newAuthor);
						authors.Add(newAuthor);
					}

					foreach (var author in authors)
					{
						manga.Authors.Add(author);
					}
				}

				manga.Genres.Clear();
				if (mangaDTO.GenreNames != null && mangaDTO.GenreNames.Any())
				{
					var genres = await _context.Genres
						.Where(g => mangaDTO.GenreNames.Contains(g.Name))
						.ToListAsync();

					var newGenreNames = mangaDTO.GenreNames.Except(genres.Select(g => g.Name)).ToList();
					foreach (var newGenreName in newGenreNames)
					{
						var newGenre = new Genre { Name = newGenreName };
						await _context.Genres.AddAsync(newGenre);
						genres.Add(newGenre);
					}

					foreach (var genre in genres)
					{
						manga.Genres.Add(genre);
					}
				}

				await _context.SaveChangesAsync();

				return new MangaDTO
				{
					Id = manga.Id,
					Title = manga.Title,
					Status = manga.Status.Name,
					Description = manga.Description,
					ReleaseDate = manga.ReleaseDate,
					ThumbnailUrl = manga.ThumbnailUrl,
					AuthorNames = manga.Authors.Select(a => a.Name).ToList(),
					GenreNames = manga.Genres.Select(g => g.Name).ToList()
				};
			}
			catch
			{
				throw;
			}
		}


		public async Task<List<ChapterDTO>> GetChaptersByMangaIdAsync(int mangaId)
		{
			return await _context.Chapters
				.Where(c => c.MangaId == mangaId)
				.Select(c => new ChapterDTO
				{
					Id = c.Id,
					MangaId = c.MangaId,
					Title = c.Title,
					Number = c.Number,
					ReleaseDate = c.ReleaseDate
				})
				.ToListAsync();
		}

		public async Task<bool> DeleteMangaAsync(int id)
		{
			try
			{
				var manga = await _context.Mangas.FindAsync(id);

				if (manga == null)
				{
					return false;
				}

				_context.Mangas.Remove(manga);
				return true;
			}
			catch
			{
				return false;
			}
			finally
			{
				await _context.SaveChangesAsync();
			}
		}
		public async Task<ChapterDTO> CreateChapterAsync(int mangaId, ChapterDTO chapterDTO)
		{
			var manga = await _context.Mangas.FindAsync(mangaId);
			if (manga == null)
			{
				throw new ArgumentException("Mangá não encontrado.");
			}

			var chapter = new Chapter
			{
				MangaId = mangaId,
				Title = chapterDTO.Title,
				Number = chapterDTO.Number,
				ReleaseDate = chapterDTO.ReleaseDate
			};

			_context.Chapters.Add(chapter);
			await _context.SaveChangesAsync();

			return new ChapterDTO
			{
				Id = chapter.Id,
				MangaId = chapter.MangaId,
				Title = chapter.Title,
				Number = chapter.Number,
				ReleaseDate = chapter.ReleaseDate
			};
		}
	}
}