using Gramin_Bazzar_marketplace_for_rural_Nepal_.Areas.Identity.Data;
using Gramin_Bazzar_marketplace_for_rural_Nepal_.Models;
using Gramin_Bazzar_marketplace_for_rural_Nepal_.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Gramin_Bazzar_marketplace_for_rural_Nepal_.Controllers
{
    public class EditDeleteController : Controller
    {
        private readonly GraminDBContext context;
        public EditDeleteController(GraminDBContext context)
        {

            this.context = context;
        }
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                var product = context.Products
              .Include(p => p.Category)
               .Include(p => p.State)
              .Include(p => p.District)
              .FirstOrDefault(p => p.ProductId == id);

                if (product != null)
                {


                    return View(product);
                }
            }
            return RedirectToAction("List", "ProductGM");
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int? id)
        {
            var product = context.Products.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            context.Products.Remove(product);
            context.SaveChanges();

            TempData["Message"] = "Product deleted successfully.";
            return RedirectToAction("List", "ProductGM");
        }
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                var product = context.Products
              .Include(p => p.Category)
               .Include(p => p.State)
              .Include(p => p.District)
              .FirstOrDefault(p => p.ProductId == id);

                if (product != null)
                {


                    return View(product);
                }
            }
            return RedirectToAction("List", "ProductGM");
        }
       
    



    }

}