using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Gramin_Bazzar_marketplace_for_rural_Nepal_.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization; // Add this

namespace Gramin_Bazzar_marketplace_for_rural_Nepal_.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager; //  Change here
      
        public UserRolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager) // <-- And here
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // List all roles
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        // Display role creation form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create role (save to database)
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                if (!await _roleManager.RoleExistsAsync(model.Name))
                {
                    await _roleManager.CreateAsync(new IdentityRole(model.Name));
                    TempData["Success"] = "Role created successfully!";
                }
                else
                {
                    TempData["Error"] = "Role already exists!";
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
