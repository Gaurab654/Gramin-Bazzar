using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mono.TextTemplating;

namespace Gramin_Bazzar_marketplace_for_rural_Nepal_.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ImageName { get; set; }
        [Required]
       public String? ProductName { get; set; }
        [Required]
        public  String? Description { get; set; }
        [Required]
        public     int? Price { get; set; }
        [Display(Name = "Stock Quantity in Kg")]
        [Required]
        public   decimal? StockQuantity { get; set; }
        
        public  int? CategoryId { get; set; }
        [Required]
        public string? SellerName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
  
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits")]
        public string? PhoneNumber { get; set; }

        [Required]
        public int? StateId { get; set; }

        [Required]
        public int? DistrictId { get; set; }
        //Navigarion property
        [ForeignKey("CategoryId")]
        public Category ?Category { get; set; }
        [ForeignKey("DistrictId")]
        public virtual District? District { get; set; }
        [ForeignKey("StateId")]
        public virtual State? State { get; set; }



    }
}
