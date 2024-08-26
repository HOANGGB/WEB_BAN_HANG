using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using WEB.Models;
using System.Text;
using Azure;
using Azure.Core;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;


        public HomeController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            if(HttpContext.Session.GetString("User") == null)
            {
                return RedirectToAction(nameof(Login));
            }
            List<FoodItem> ListFoodItem = new List<FoodItem>();
            var response = await _httpClient.GetAsync("https://localhost:7221/api/FoodItemAPI");

            if (response.IsSuccessStatusCode)
            {
                var apiFoodItem = await response.Content.ReadAsStringAsync();
                ListFoodItem = JsonConvert.DeserializeObject<List<FoodItem>>(apiFoodItem);
            }
            else
            {

            }

            return View(ListFoodItem);
        }
        public IActionResult Edit()
        {
            if (HttpContext.Session.GetString("User") == null)
            {
                return RedirectToAction(nameof(Login));
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Login(User user)
        {
                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://localhost:7221/api/UserAPI/Login", content);

            if (response.IsSuccessStatusCode)
            {

                var userContent = await response.Content.ReadAsStringAsync();
                var loggedInUser = JsonConvert.DeserializeObject<User>(userContent);
                HttpContext.Session.SetString("User", JsonConvert.SerializeObject(loggedInUser));
                return RedirectToAction(nameof(Index));
            }
            else return View();
        }


        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}