using API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB.Models;


namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailAPIController : ControllerBase
    {
        private readonly UserDbContext db;
        public OrderDetailAPIController(UserDbContext DbContext)
        {
            db = DbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var orderDetail = db.OrderDetail.ToList();
            return Ok(orderDetail);
        }
        [HttpGet("{id}")]
        public IActionResult Detail(int id)
        {
            var orderDetail = db.OrderDetail.FirstOrDefault(x => x.OrderDetailId == id);
            if (orderDetail != null) return Ok(orderDetail);
            else return NotFound();
        }
        [HttpPost]
        public IActionResult Create(OrderDetail orderDetail)
        {
            var newOrderDetail = new OrderDetail
            {
                OrderId= orderDetail.OrderId,
                FoodItemId= orderDetail.FoodItemId,
                Quantity= orderDetail.Quantity,
                TotalPrice= orderDetail.TotalPrice,

            };
            db.OrderDetail.Add(newOrderDetail);
            db.SaveChanges();
            return Ok(newOrderDetail);
        }

        [HttpPut]
        public IActionResult Edit(int id, OrderDetail orderDetail)
        {
            var orderDetailEdit = db.OrderDetail.FirstOrDefault(x => x.OrderDetailId == id);
            if (orderDetailEdit != null)
            {
                orderDetailEdit.OrderId = orderDetail.OrderId;
                orderDetailEdit.FoodItemId = orderDetail.FoodItemId;
                orderDetailEdit.Quantity = orderDetail.Quantity;
                orderDetailEdit.TotalPrice= orderDetail.TotalPrice;
                db.SaveChanges();
                return Ok();
            }
            else return NotFound();

        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var orderDetailDelete = db.OrderDetail.FirstOrDefault(x => x.OrderDetailId == id);
            if (orderDetailDelete != null)
            {
                db.OrderDetail.Remove(orderDetailDelete);
                db.SaveChanges();
                return Ok();
            }
            else return NotFound();
        }
    }
}
