using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WEB.Models;

namespace WEB.Controllers
{
    public class ComboController : Controller
    {
        private readonly HttpClient _httpClient;

        public ComboController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            List<Combo> ListCombo = new List<Combo>();
            var Combo = await _httpClient.GetAsync("https://localhost:7221/api/ComboAPI\r\n");
            if (Combo.IsSuccessStatusCode)
            {
                var apiCombo = await Combo.Content.ReadAsStringAsync();
                ListCombo = JsonConvert.DeserializeObject<List<Combo>>(apiCombo);
            }
            return View(ListCombo);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Combo Combo)
        {
            var content = new StringContent(JsonConvert.SerializeObject(Combo), Encoding.UTF8, "application/json");
            var url = await _httpClient.PostAsync($"https://localhost:7221/api/ComboAPI", content);
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var apiResponse = await _httpClient.GetAsync($"https://localhost:7221/api/ComboAPI/{id}");

            if (apiResponse.IsSuccessStatusCode)
            {
                var api = await apiResponse.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<Combo>(api);
                return View(user);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Edit(Combo Combo)
        {
            var content = new StringContent(JsonConvert.SerializeObject(Combo), Encoding.UTF8, "application/json");
            var url = await _httpClient.PutAsync("https://localhost:7221/api/ComboAPI", content);
            if (url.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(Combo);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7221/api/ComboAPI?id={id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
