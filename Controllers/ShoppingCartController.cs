using Gramin_Bazzar_marketplace_for_rural_Nepal_.Areas.Identity.Data;
using Gramin_Bazzar_marketplace_for_rural_Nepal_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Gramin_Bazzar_marketplace_for_rural_Nepal_.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly GraminDBContext context;

        public ShoppingCartController(GraminDBContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddToCart(int? id)
        {
            if (id == null)
                return BadRequest();

            var productToAdd = context.Products.Find(id);

            if (productToAdd == null)
                return NotFound();

            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();
            var existingCartItem = cartItems.FirstOrDefault(b => b.Product.ProductId == id);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity++;
            }
            else
            {
                cartItems.Add(new ShoppingCartItem
                {
                    Product = productToAdd,
                    Quantity = 1
                });
            }

            HttpContext.Session.Set("Cart", cartItems);
            return RedirectToAction("ViewCart");
        }

        public IActionResult ViewCart()
        {
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var cartViewModel = new ShoppingCartViewModel
            {
                CartItems = cartItems,
                TotalPrice = cartItems.Sum(item => item.Product.Price * item.Quantity)
            };

            return View(cartViewModel);
        }
        [HttpPost]
        public IActionResult RemoveItem(int id)
        {
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();
            var itemToRemove = cartItems.FirstOrDefault(item => item.Product.ProductId == id);

            if (itemToRemove != null)
            {
                if (itemToRemove.Quantity > 1)
                {
                    itemToRemove.Quantity--;
                }
                else
                {
                    cartItems.Remove(itemToRemove);
                }
            }

            HttpContext.Session.Set("Cart", cartItems);
            return RedirectToAction("ViewCart");
        }
    }
}
