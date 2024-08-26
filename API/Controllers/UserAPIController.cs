using API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using WEB.Models;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private readonly UserDbContext db;
        private readonly IConfiguration _configuration;

        public UserAPIController(UserDbContext userDbContext, IConfiguration configuration)
        {
            db = userDbContext;
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult GetAllUser()
        {
            var user = db.Users.ToList();
            return Ok(user);
        }
        [HttpGet("{id}")]
        public IActionResult Detail(int id)
        {
            var user = db.Users.FirstOrDefault(x=>x.UserId == id);
            if(user != null)return Ok(user);
            else return NotFound();
        }
        [HttpPost]
        public IActionResult CreateUser(User user)
        {

            var newUser = new User
            {
                UserId = user.UserId,
                Username = user.Username,
                FullName = user.FullName,
                Email = user.Email,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Role = user.Role
                };

                db.Users.Add(newUser);
                db.SaveChanges();
                return Ok(newUser);
        }

        [HttpPut]
        public IActionResult EditUser(User user)
        {
            var userEdit = db.Users.FirstOrDefault(x=>x.UserId==user.UserId);
            if(userEdit != null)
            {
/*                userEdit.UserId= user.UserId;*/                
                userEdit.Username= user.Username;
                userEdit.FullName= user.FullName;
                userEdit.Email= user.Email;
                userEdit.Password= user.Password;
                userEdit.PhoneNumber= user.PhoneNumber;
                userEdit.Address= user.Address;
                user.Role = user.Role;

                db.SaveChanges();
                return Ok(userEdit);
            }else return NotFound();

        }
        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            var userDelete = db.Users.FirstOrDefault(x=>x.UserId== id);
            if(userDelete != null)
            {
                db.Users.Remove(userDelete);
                db.SaveChanges();
                return Ok();
            }else return NotFound();
        }

        [HttpPost("Login")]
        public IActionResult CheckUser(User user)
        {
            var userr = db.Users.FirstOrDefault(u => u.Username == user.Username);
            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", userr.UserId);
                var token = GenerateJwtToken(userr);
                return Ok(new { Token = token, User = userr });
            }
            else return BadRequest("User Is Null");
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
