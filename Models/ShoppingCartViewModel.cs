namespace Gramin_Bazzar_marketplace_for_rural_Nepal_.Models
{
    public class ShoppingCartViewModel
    {
        public List<ShoppingCartItem> CartItems { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? TotalQuantity { get; set; }
    }
}
