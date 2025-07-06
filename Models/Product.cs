using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gramin_Bazzar_marketplace_for_rural_Nepal_.Models
{
    public class Product
    {
        
        public string? PreviewImage { get; set; }

        public int productId;
       public String? ProductName { get; set; }
      public  String? Description { get; set; }
       public     int? Price { get; set; }
        [Display(Name = "Stock Quantity in Kg")]
        public   decimal? StockQuantity { get; set; }
      public  String? Category { get; set; }


    }
}
