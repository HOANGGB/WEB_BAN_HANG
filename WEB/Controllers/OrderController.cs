using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WEB.Models;

namespace WEB.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient _httpClient;

        public OrderController(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<IActionResult> Index()
        {
            List<Order> ListOrder = new List<Order>();
            var Order = await _httpClient.GetAsync("https://localhost:7221/api/OrderAPI\r\n");
            if (Order.IsSuccessStatusCode)
            {
                var apiOrder = await Order.Content.ReadAsStringAsync();
                ListOrder = JsonConvert.DeserializeObject<List<Order>>(apiOrder);
            }
            return View(ListOrder);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(int FoodItemId)
        {
            var content = new StringContent(JsonConvert.SerializeObject(FoodItemId), Encoding.UTF8, "application/json");
            var url = await _httpClient.PostAsync($"https://localhost:7221/api/OrderAPI?FoodItemId={FoodItemId}", content);
            if (url.IsSuccessStatusCode)
            {
                @ViewBag.Mess = "Order Is Success";
                return RedirectToAction("Index", "Home"); 
            }

            return RedirectToAction("Index", "Home"); 
        }

        public IActionResult Edit()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var apiResponse = await _httpClient.GetAsync($"https://localhost:7221/api/OrderAPI/{id}");

            if (apiResponse.IsSuccessStatusCode)
            {
                var api = await apiResponse.Content.ReadAsStringAsync();
                var order = JsonConvert.DeserializeObject<Order>(api);
                return View(order);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Order Order)
        {
            var content = new StringContent(JsonConvert.SerializeObject(Order), Encoding.UTF8, "application/json");
            var url = await _httpClient.PutAsync($"https://localhost:7221/api/OrderAPI", content);
            if (url.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7221/api/OrderAPI?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Edit));
            }
        }
    }
}
