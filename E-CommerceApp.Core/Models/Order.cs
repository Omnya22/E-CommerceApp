using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_CommerceApp.Core.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Display(Name = "Order Status")]
        public OrderStatus OrderStatus { get; set; }
        
        public ICollection<Product> Products { get; set; }
    }
}
