using Microsoft.AspNetCore.Mvc;

namespace CS68_MVC1
{
    [Area("ProductManage")]
    public class ProductController:Controller
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService=productService;
        }
        // [Route("/cac-san-pham")]
        public IActionResult Index()
        {
            var p=_productService.products.OrderBy(p=>p.price).ToList();
            return View(p);
        }
    }
}