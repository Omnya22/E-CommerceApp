using System.ComponentModel.DataAnnotations;

namespace E_CommerceApp.Core.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        [Display(Name = "Product Title")]
        public string Name { get; set; }

        [MaxLength(200)]
        [Display(Name = "Product Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Product Price")]
        public decimal Price { get; set; }

        [Display(Name = "Product Photo")]
        public string PhotoUrl { get; set; }
    }
}
