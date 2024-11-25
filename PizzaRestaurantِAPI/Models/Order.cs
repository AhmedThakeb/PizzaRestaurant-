using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaRestaurantِAPI.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Pizza")]
        public int PizzaId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public Pizza Pizza { get; set; }
    }

}
