using API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB.Models;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboDetailAPIController : ControllerBase
    {
        private readonly UserDbContext db;
        public ComboDetailAPIController(UserDbContext DbContext)
        {
            db = DbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var comboDetail = db.ComboDetail.ToList();
            return Ok(comboDetail);
        }
        [HttpGet("{id}")]
        public IActionResult Detail(int id)
        {
            var comboDetail = db.ComboDetail.FirstOrDefault(x => x.ComboDetailId == id);
            if (comboDetail != null) return Ok(comboDetail);
            else return NotFound();
        }
        [HttpPost]
        public IActionResult Create(ComboDetail comboDetail)
        {
            var newCombo = new ComboDetail
            {
                ComboId = comboDetail.ComboId,
                FoodItemId = comboDetail.FoodItemId

            };
            db.ComboDetail.Add(newCombo);
            db.SaveChanges();
            return Ok(newCombo);
        }

        [HttpPut]
        public IActionResult Edit(int id, ComboDetail comboDetail)
        {
            var ComboDetailEdit = db.ComboDetail.FirstOrDefault(x => x.ComboDetailId == id);
            if (ComboDetailEdit != null)
            {
                ComboDetailEdit.ComboId = comboDetail.ComboId;
                ComboDetailEdit.FoodItemId= comboDetail.FoodItemId;
                db.SaveChanges();
                return Ok();
            }
            else return NotFound();

        }
        [HttpDelete]
        public IActionResult DeleteCombo(int id)
        {
            var comboDetailDelete = db.ComboDetail.FirstOrDefault(x => x.ComboDetailId == id);
            if (comboDetailDelete != null)
            {
                db.ComboDetail.Remove(comboDetailDelete);
                db.SaveChanges();
                return Ok();
            }
            else return NotFound();
        }
    }
}
