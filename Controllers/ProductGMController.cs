
using Gramin_Bazzar_marketplace_for_rural_Nepal_.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gramin_Bazzar_marketplace_for_rural_Nepal_.Controllers
{
    public class ProductGMController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult<string> Add(Product product)
        {
            return "nepal is a land locked coutry";
        }
    }
}
