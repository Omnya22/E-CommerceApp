using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceApp.Core.Models
{
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Display(Name = "Product Title")]
        public string Name { get; set; }

        [MaxLength(200)]
        [Display(Name = "Product Description")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Product Price")]
        public decimal Price { get; set; }

        [Display(Name = "Product Photo")]
        public string PhotoUrl { get; set; }

        public IList<OrderProduct> OrderProducts { get; set; }
    }
}
