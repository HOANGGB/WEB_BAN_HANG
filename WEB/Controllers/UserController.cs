using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WEB.Models;

namespace WEB.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;

        public UserController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            List<User> ListUser = new List<User>();
            var User = await _httpClient.GetAsync("https://localhost:7221/api/UserAPI");
            if (User.IsSuccessStatusCode)
            {
                var apiUser = await User.Content.ReadAsStringAsync();
                ListUser = JsonConvert.DeserializeObject<List<User>>(apiUser);
            }
            return View(ListUser);
        }


        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {

                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://localhost:7221/api/UserAPI", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }

        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var apiResponse = await _httpClient.GetAsync($"https://localhost:7221/api/UserAPI/{id}");

            if (apiResponse.IsSuccessStatusCode)
            {
                var apiUser = await apiResponse.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(apiUser);
                return View(user);
            }
            else
            {
                return RedirectToAction(nameof(Index)); 
            }
        }

        public async Task<IActionResult> Edit(User User)
        {
            var content = new StringContent(JsonConvert.SerializeObject(User), Encoding.UTF8, "application/json");
            var url = await _httpClient.PutAsync($"https://localhost:7221/api/UserAPI", content);
            if (url.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }else return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var apiResponse = await _httpClient.DeleteAsync($"https://localhost:7221/api/UserAPI?id={id}");

            if (apiResponse.IsSuccessStatusCode)
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
