using API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.Models;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemAPIController : ControllerBase
    {
        private readonly UserDbContext db;
        public FoodItemAPIController(UserDbContext DbContext)
        {
            db = DbContext;
        }
        [HttpGet]
        public  IActionResult GetAll()
        {
            var foodItem =  db.FoodItem.ToList();
            return Ok(foodItem);
        }
        [HttpGet("{id}")]
        public IActionResult Detail(int id)
        {
            var foodItemDetail = db.FoodItem.FirstOrDefault(x => x.FoodItemId == id);
            if (foodItemDetail != null) return Ok(foodItemDetail);
            else return NotFound();
        }
        [HttpPost]
        public IActionResult Create(FoodItem foodItem)
        {
            var newFoodItem = new FoodItem
            {
               FoodItemId= foodItem.FoodItemId,
               Name= foodItem.Name,
               Price= foodItem.Price,
               Description= foodItem.Description,
               ImageUrl= foodItem.ImageUrl,
               Category = foodItem.Category,
            };
            db.FoodItem.Add(newFoodItem);
            db.SaveChanges();
            return Ok(newFoodItem);
        }

        [HttpPut]
        public IActionResult Edit(FoodItem foodItem)
        {
            var foodItemEdit = db.FoodItem.FirstOrDefault(x => x.FoodItemId == foodItem.FoodItemId);
            if (foodItemEdit != null)
            {
                foodItemEdit.Name = foodItem.Name;
                foodItemEdit.Price = foodItem.Price;
                foodItemEdit.Description = foodItem.Description;
                foodItemEdit.ImageUrl = foodItem.ImageUrl;
                foodItemEdit.Category = foodItem.Category;
                db.SaveChanges();
                return Ok();
            }
            else return NotFound();

        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var foodItemDelete = db.FoodItem.FirstOrDefault(x => x.FoodItemId == id);
            if (foodItemDelete != null)
            {
                db.FoodItem.Remove(foodItemDelete);
                db.SaveChanges();
                return Ok();
            }
            else return NotFound();
        }
    }
}
