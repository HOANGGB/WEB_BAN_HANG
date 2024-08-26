using API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEB.Models;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderAPIController : ControllerBase
    {
        private readonly UserDbContext db;
        public OrderAPIController(UserDbContext DbContext)
        {
            db = DbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var order = db.Order.ToList();
            return Ok(order);
        }
        [HttpGet("{id}")]
        public IActionResult Detail(int id)
        {
            var orderDetail = db.Order.FirstOrDefault(x => x.OrderId == id);
            if (orderDetail != null) return Ok(orderDetail);
            else return NotFound();
        }
        [HttpPost]
        public IActionResult Create(int FoodItemId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var newOrder = new Order
            {

                 UserId = (int)userId,
                OrderDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
                Status = "Pending"
            };
            db.Order.Add(newOrder);
            db.SaveChanges();

            var newOrderDetail = new OrderDetail
            {
                OrderId = newOrder.OrderId,
                FoodItemId = FoodItemId,
                Quantity = 1,
                TotalPrice = db.FoodItem.FirstOrDefault(x => x.FoodItemId == FoodItemId).Price,
            };
            db.Add(newOrderDetail);
            db.SaveChanges();
            return Ok(newOrder);
        }

        [HttpPut]
        public IActionResult Edit(Order order)
        {
            var orderEdit = db.Order.FirstOrDefault(x => x.OrderId == order.OrderId);
            if (orderEdit != null)
            {
                orderEdit.UserId = order.UserId;
                orderEdit.OrderDate = order.OrderDate;
                orderEdit.Status = order.Status;
                db.SaveChanges();
                return Ok();
            }
            else return NotFound();

        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var orderDelete = db.Order.FirstOrDefault(x => x.OrderId == id);
            if (orderDelete != null)
            {
                db.Order.Remove(orderDelete);
                db.SaveChanges();
                return Ok();
            }
            else return NotFound();
        }
    }
}
