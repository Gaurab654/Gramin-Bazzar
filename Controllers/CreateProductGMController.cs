using Gramin_Bazzar_marketplace_for_rural_Nepal_.Areas.Identity.Data;
using Gramin_Bazzar_marketplace_for_rural_Nepal_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol;

namespace Gramin_Bazzar_marketplace_for_rural_Nepal_.Controllers
{
    public class CreateProductGMController : Controller
    {

        private readonly GraminDBContext context;
        public CreateProductGMController(GraminDBContext context)
        {

            this.context = context;
        }
        
        public IActionResult Create()
        {
            var states = context.States.ToList();
            ViewBag.Categories = new SelectList(context.Categories.ToList(), "CategoryId", "CategoryType");
            ViewBag.States = new SelectList(states, "StateId", "StateName");
            return View();
        }
        public JsonResult GetDistrict(int id)
        {
            var dist = context.Districts
                              .Where(x => x.StateId == id)
                              .Select(x => new
                              {
                                  distId = x.DistrictId,
                                  distName = x.DistrictName
                              })
                              .ToList();

            return Json(dist);
        }
        [HttpPost]
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                context.Products.Add(product);
                context.SaveChanges();
                return RedirectToAction("List", "ProductGM");
            }

            //  Repopulate the dropdowns because we're returning the form again
            ViewBag.Categories = new SelectList(context.Categories.ToList(), "CategoryId", "CategoryType");
            ViewBag.States = new SelectList(context.States.ToList(), "StateId", "StateName");

            return View(product); // re-render form with errors and filled dropdowns
        }


    }
}
