using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaRestaurantِAPI.Data;
using PizzaRestaurantِAPI.Models;

namespace PizzaRestaurantِAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet] public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            return _context.Customers.ToList(); 
        }
        [HttpGet("{id}")] public ActionResult<Customer> GetCustomer(int id)
        { 
            var customer = _context.Customers.Find(id);
            if (customer == null)
            { return NotFound(); }
            return customer;
        }
        [HttpPost] public ActionResult<Customer> PostCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }
        [HttpPut("{id}")] public IActionResult PutCustomer(int id, Customer customer) 
        { if (id != customer.Id)
            { return BadRequest(); }
            _context.Entry(customer).State = EntityState.Modified;
            try { _context.SaveChanges(); } 
            catch (DbUpdateConcurrencyException) 
            { if (!_context.Customers.Any(e => e.Id == id)) 
                { return NotFound(); } 
                else { throw; } }
            return NoContent();
        }
        [HttpDelete("{id}")] public IActionResult DeleteCustomer(int id)
        { 
            var customer = _context.Customers.Find(id);
            if (customer == null)
            { return NotFound(); } 
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return NoContent(); 
        }
    }
}
