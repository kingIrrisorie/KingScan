using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using frontend.Models;

namespace frontend.Controllers
{
    public class MangaController : Controller
    {
        private readonly HttpClient _httpClient;
		public MangaController(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("http://localhost:5215");
		}


		// GET: MangaController
		[Route("Manga/{Id}")]
        public async Task<ActionResult> Index(int Id)
        {
			try
			{
				var manga = await GetMangaAsync(Id);
				if (manga == null)
					return NotFound();

				return View(manga);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Erro ao obter mangá");
			}
		}

        public async Task<Manga> GetMangaAsync(int Id)
        {
            Manga manga = null;

            string requestUrl = $"api/Mangas/{Id}";

            try
            {
				HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
				response.EnsureSuccessStatusCode();
				var mangaJson = await response.Content.ReadAsStringAsync();
                manga = JsonConvert.DeserializeObject<Manga>(mangaJson);
                return manga;
            }
			catch (HttpRequestException ex)
			{
				throw new Exception($"Erro na requisição HTTP: {ex.Message}", ex);
			}
			catch (JsonException ex)
			{
				throw new Exception($"Erro na deserialização JSON: {ex.Message}", ex);
			}

		}
        /*
        // GET: MangaController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MangaController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MangaController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MangaController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MangaController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MangaController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MangaController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
