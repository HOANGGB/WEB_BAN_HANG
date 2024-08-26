using API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboAPIController : ControllerBase
    {
        private readonly UserDbContext db;
        public ComboAPIController(UserDbContext DbContext)
        {
            db = DbContext;
        }
        [HttpGet]
        public IActionResult GetAllUser()
        {
            var combo = db.Combo.ToList();
            return Ok(combo);
        }
        [HttpGet("{id}")]
        public IActionResult Detail(int id)
        {
            var comboDetail = db.Combo.FirstOrDefault(x => x.ComboId == id);
            if (comboDetail != null) return Ok(comboDetail);
            else return NotFound();
        }
        [HttpPost]
        public IActionResult CreateCombo(Combo combo)
        {
            var newCombo = new Combo
            {
                Name = combo.Name,
                Description = combo.Description,
                Price = combo.Price,
            };
            db.Combo.Add(newCombo);
            db.SaveChanges();
            return Ok(newCombo);
        }

        [HttpPut]
        public IActionResult EditCombo(Combo combo)
        {
            var ComboEdit = db.Combo.FirstOrDefault(x => x.ComboId == combo.ComboId);
            if (ComboEdit != null)
            {
                ComboEdit.Name= combo.Name;
                ComboEdit.Description= combo.Description;
                ComboEdit.Price= combo.Price;
                db.SaveChanges();
                return Ok();
            }
            else return NotFound();

        }
        [HttpDelete]
        public IActionResult DeleteCombo(int id)
        {
            var comboDelete = db.Combo.FirstOrDefault(x => x.ComboId == id);
            if (comboDelete != null)
            {
                db.Combo.Remove(comboDelete);
                db.SaveChanges();
                return Ok();
            }
            else return NotFound();
        }
    }
}
