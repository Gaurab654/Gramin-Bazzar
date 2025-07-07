
using Gramin_Bazzar_marketplace_for_rural_Nepal_.Areas.Identity.Data;
using Gramin_Bazzar_marketplace_for_rural_Nepal_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gramin_Bazzar_marketplace_for_rural_Nepal_.Controllers
{

    public class ProductGMController : Controller
    {
        private readonly GraminDBContext context;
        public ProductGMController( GraminDBContext context)
        {
           
            this.context = context;
        }
        [HttpGet]
        public IActionResult List()
        {
            var products = context.Products
                .Include(p => p.Category)
                .Include(p => p.State)
                .Include(p => p.District)
                .ToList();

            return View(products);
        }

    }
}
