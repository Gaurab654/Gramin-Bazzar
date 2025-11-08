using Gramin_Bazzar_marketplace_for_rural_Nepal_.Areas.Identity.Data;
using Gramin_Bazzar_marketplace_for_rural_Nepal_.Models;
using Gramin_Bazzar_marketplace_for_rural_Nepal_.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol;

namespace Gramin_Bazzar_marketplace_for_rural_Nepal_.Controllers
{
    [Authorize(Roles = "Admin,Seller")]
    public class CreateProductGMController : Controller
    {
        private readonly GraminDBContext context;
        private readonly IWebHostEnvironment env;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateProductGMController(GraminDBContext context, IWebHostEnvironment env, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.env = env;
            _userManager = userManager;
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
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = null;

                if (model.Image!= null)
                {
                    String f = Path.Combine(env.WebRootPath, "ProductPhoto");
                    fileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string filePath=Path.Combine(f, fileName);
                    model.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                var user = await _userManager.GetUserAsync(User);

                Product product = new Product()
                {
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
                    ImageName = fileName,
                    SellerId = user.Id
                };

                context.Products.Add(product);
                context.SaveChanges();

                TempData["Message"] = "New product was added successfully.";
                return RedirectToAction("List", "ProductGM");
            }

            TempData["Message"] = "Please select image(s).";
            return View(model);
        }
    }
}
