using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WEB.Models;

namespace WEB.Controllers
{
    public class FoodItemController : Controller
    {
        private readonly HttpClient _httpClient;

        public FoodItemController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            List<FoodItem> ListFoodItem = new List<FoodItem>();
            var foodItem = await _httpClient.GetAsync("https://localhost:7221/api/FoodItemAPI\r\n");
            if (foodItem.IsSuccessStatusCode)
            {
                var apiFoodItem = await foodItem.Content.ReadAsStringAsync();
                ListFoodItem = JsonConvert.DeserializeObject<List<FoodItem>>(apiFoodItem);
            }
            return View(ListFoodItem);
        }
        [HttpPost]
        public async Task<IActionResult> Create(FoodItem foodItem)
        {
            var content = new StringContent(JsonConvert.SerializeObject(foodItem), Encoding.UTF8, "application/json");
            var url = await _httpClient.PostAsync($"https://localhost:7221/api/FoodItemAPI", content);
            if (url.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
            else return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var apiResponse = await _httpClient.GetAsync($"https://localhost:7221/api/FoodItemAPI/{id}");

            if (apiResponse.IsSuccessStatusCode)
            {
                var api = await apiResponse.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<FoodItem>(api);
                return View(user);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
        public async Task<IActionResult> Edit(FoodItem foodItem)
        {
            var content = new StringContent(JsonConvert.SerializeObject(foodItem), Encoding.UTF8, "application/json");
            var url = await _httpClient.PutAsync($"https://localhost:7221/api/FoodItemAPI", content);
            if (url.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7221/api/FoodItemAPI?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
