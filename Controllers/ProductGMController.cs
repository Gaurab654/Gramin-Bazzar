using Gramin_Bazzar_marketplace_for_rural_Nepal_.Areas.Identity.Data;
using Gramin_Bazzar_marketplace_for_rural_Nepal_.Models;
using Gramin_Bazzar_marketplace_for_rural_Nepal_.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Gramin_Bazzar_marketplace_for_rural_Nepal_.Controllers
{
    [Authorize]
    public class ProductGMController : Controller
    {
        private readonly GraminDBContext _context;
        private readonly RecommendationService _recommendationService;

        public ProductGMController(GraminDBContext context, RecommendationService recommendationService)
        {
            _context = context;
            _recommendationService = recommendationService;
        }

        [HttpGet]
        public IActionResult List()
        {
            // Get all products with category, state, district
            var products = _context.Products
                .Include(p => p.Category)
                .Include(p => p.State)
                .Include(p => p.District)
                .ToList();

            // Prepare a dictionary of recommendations per product
            var recommendations = products.ToDictionary(
                p => p.ProductId,
                p => _recommendationService.GetSimilarProducts(p.ProductId, topN: 3) // top 3 recommended
            );

            // Pass both products and recommendations to view using ViewBag
            ViewBag.Recommendations = recommendations;

            return View(products);
        }
    }
}
