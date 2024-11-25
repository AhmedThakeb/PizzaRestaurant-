using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaRestaurantِAPI.Data;
using PizzaRestaurantِAPI.Models;

namespace PizzaRestaurantِAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
      
       
        
        public PizzaController(ApplicationDbContext context) 
        { 
            _context = context;
        }
        private readonly ApplicationDbContext _context;
        [HttpGet]
        public ActionResult<IEnumerable<Pizza>> GetPizzas()
        {
            return _context.Pizzas.ToList(); 
        }
        [HttpGet("{id}")]
        public ActionResult<Pizza> GetPizza(int id)
        { 
            var pizza = _context.Pizzas.Find(id);
            if (pizza == null)
            { return NotFound(); }
            return pizza;
        }
        [HttpPost]
        public ActionResult<Pizza> PostPizza(Pizza pizza)
        { 
            _context.Pizzas.Add(pizza);
            _context.SaveChanges();
            return CreatedAtAction("GetPizza", new { id = pizza.Id }, pizza);
        }
        [HttpPut("{id}")] 
        public IActionResult PutPizza(int id, Pizza pizza)
        {
            if (id != pizza.Id)
            { return BadRequest(); } 
            _context.Entry(pizza).State = EntityState.Modified;
            try { _context.SaveChanges(); }
            catch (DbUpdateConcurrencyException)
            { if (!_context.Pizzas.Any(e => e.Id == id))
                { return NotFound(); }
                else { throw; } }
            return NoContent(); }
        [HttpDelete("{id}")]
        public IActionResult DeletePizza(int id)
        {
            var pizza = _context.Pizzas.Find(id);
            if (pizza == null)
            { return NotFound(); }
            _context.Pizzas.Remove(pizza);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
