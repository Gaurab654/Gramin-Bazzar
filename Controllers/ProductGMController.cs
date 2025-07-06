
using Gramin_Bazzar_marketplace_for_rural_Nepal_.Areas.Identity.Data;
using Gramin_Bazzar_marketplace_for_rural_Nepal_.Models;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }
        [HttpPost]
        public ActionResult<string> Add(Product product)
        {
            return "nepal is a land locked coutry";
        }
    }
}
