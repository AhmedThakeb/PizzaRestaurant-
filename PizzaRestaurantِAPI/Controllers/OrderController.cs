using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaRestaurantِAPI.Data;
using PizzaRestaurantِAPI.Models;

namespace PizzaRestaurantِAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet] public ActionResult<IEnumerable<Order>> GetOrders() 
        {
            return _context.Orders.ToList();
        }
        [HttpGet("{id}")] public ActionResult<Order> GetOrder(int id)
        { 
            var order = _context.Orders.Find(id);
            if (order == null)
            { return NotFound(); }
            return order; 
        }
        [HttpPost] public ActionResult<Order> PostOrder(Order order)
        { 
            _context.Orders.Add(order);
            _context.SaveChanges();
            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }
        [HttpPut("{id}")] public IActionResult PutOrder(int id, Order order)
        {
            if (id != order.Id)
            { return BadRequest(); }
            _context.Entry(order).State = EntityState.Modified;
            try { _context.SaveChanges(); } 
            catch (DbUpdateConcurrencyException) 
            { if (!_context.Orders.Any(e => e.Id == id)) 
                { return NotFound(); }
                else { throw; } 
            }
            return NoContent();
        }
        [HttpDelete("{id}")] public IActionResult DeleteOrder(int id)
        { 
            var order = _context.Orders.Find(id);
            if (order == null) 
            { return NotFound(); }
            _context.Orders.Remove(order);
            _context.SaveChanges(); 
            return NoContent(); 
        }
    }
}
