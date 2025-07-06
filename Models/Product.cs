using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Mono.TextTemplating;

namespace Gramin_Bazzar_marketplace_for_rural_Nepal_.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
       public String? ProductName { get; set; }
        [Required]
        public  String? Description { get; set; }
        [Required]
        public     int? Price { get; set; }
        [Display(Name = "Stock Quantity in Kg")]
        [Required]
        public   decimal? StockQuantity { get; set; }
        [Required]
        public  String? CategoryId { get; set; }
        [Required]
        public string? SellerName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        [Required]
        public int? StateId { get; set; }

        [Required]
        public int? DistrictId { get; set; }
     

        public virtual District District { get; set; }

        public virtual State? State { get; set; }



    }
}
