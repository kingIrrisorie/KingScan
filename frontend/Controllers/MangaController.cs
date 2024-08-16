using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using frontend.Models;

namespace frontend.Controllers
{
    public class MangaController : Controller
    {
        private readonly HttpClient _httpClient;
        public MangaController()
        {
            _httpClient = new HttpClient();
			Uri uri = new Uri("https://localhost:7160");
			_httpClient.BaseAddress = uri;
		}

        // GET: MangaController
        [Route("Manga/{Id}")]
        public async Task<ActionResult> Index(int Id)
        {
            string requestUrl = $"api/Mangas/{Id}";

            HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var mangaJson = await response.Content.ReadAsStringAsync();
                var manga = JsonConvert.DeserializeObject<Manga>(mangaJson);

				return View(manga);
            }
            else
                return View("Error");
        }

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
        }
    }
}
