using Gramin_Bazzar_marketplace_for_rural_Nepal_.Areas.Identity.Data;
using Gramin_Bazzar_marketplace_for_rural_Nepal_.Models;
using Gramin_Bazzar_marketplace_for_rural_Nepal_.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol;

namespace Gramin_Bazzar_marketplace_for_rural_Nepal_.Controllers
{
    public class CreateProductGMController : Controller
    {

        private readonly GraminDBContext context;
        private readonly IWebHostEnvironment env;

        public CreateProductGMController(GraminDBContext context, IWebHostEnvironment env)
        {

            this.context = context;
            this.env = env;
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
        public IActionResult Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFiles != null && model.ImageFiles.Any())
                {
                    List<string> savedFileNames = new List<string>();

                    string folder = Path.Combine(env.WebRootPath, "ProductPhoto");

                    foreach (var file in model.ImageFiles)
                    {
                        string fn = Guid.NewGuid().ToString() + "_Gaurab_" + file.FileName;
                        string imagepath = Path.Combine(folder, fn);

                        using (var stream = new FileStream(imagepath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        savedFileNames.Add(fn);
                    }

                    // Now create Product and set image filename(s) - e.g., join filenames if multiple or store first one
                    Product product = new Product()
                    {
                        // other properties from model
                        ProductName = model.ProductName,
                        Description = model.Description,
                        Price = model.Price,
                        StockQuantity = model.StockQuantity,
                        CategoryId = model.CategoryId,
                        SellerName = model.SellerName,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        StateId = model.StateId,
                        DistrictId = model.DistrictId,
                        ImageName = string.Join(";", savedFileNames) // if you want to save multiple names in one string
                    };

                    context.Products.Add(product);
                    context.SaveChanges();

                    TempData["Message"] = "New product was added successfully.";
                    return RedirectToAction("List", "ProductGM");
                }

                TempData["Message"] = "Please select image(s).";
                return View(model);
            }

            // Re-populate dropdowns on form redisplay
            ViewBag.Categories = new SelectList(context.Categories.ToList(), "CategoryId", "CategoryType");
            ViewBag.States = new SelectList(context.States.ToList(), "StateId", "StateName");

            return View(model);
        }
    }
}
