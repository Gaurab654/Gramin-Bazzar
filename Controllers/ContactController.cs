using Gramin_Bazzar_marketplace_for_rural_Nepal_.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gramin_Bazzar_marketplace_for_rural_Nepal_.Controllers
{
    [Authorize(Roles = "Admin,Seller,Buyer")]
    public class ContactController : Controller
    {
        private readonly GraminDBContext context;
        public ContactController(GraminDBContext context)
        {

            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
