using System.ComponentModel.DataAnnotations;

namespace PizzaRestaurantِAPI.Models
{
    public class Pizza
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Ingredients { get; set; }
    }
}
