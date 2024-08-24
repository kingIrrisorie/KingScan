using APIManga.Model;
using APIManga.DTOs;

namespace APIManga.Services
{
    public class MangaServices
    {
        public static void AddBasicParameters(Manga manga, MangaUpCreate dto)
        {
            manga.Title = dto.Title;
            manga.Status = dto.Status;
            manga.Description = dto.Description;
            manga.Released = dto.Released;
            manga.ThumbnailURL = dto.ThumbnailURL;
        }
    }
}
