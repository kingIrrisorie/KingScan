using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace frontend.Controllers
{
	public class HomeController : Controller
	{
		private readonly HttpClient _httpClient;
		public HomeController(HttpClient httpClient)
		{ 
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("https://localhost:7160");

        }
		public async Task<IActionResult> Index()
		{
			string requestUrl = $"api/Mangas";
			HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

			if (response.IsSuccessStatusCode)
			{
				var mangasJson = await response.Content.ReadAsStringAsync();
				List<Manga> mangas = JsonConvert.DeserializeObject<List<Manga>>(mangasJson);

                return View(mangas);
            }
            else
            {
                // Em caso de erro, você pode retornar uma view de erro ou uma lista vazia
                return View(new List<Manga>());
            }
        }

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
