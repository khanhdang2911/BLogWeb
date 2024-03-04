using Microsoft.AspNetCore.Mvc;
namespace CS68_MVC1.Controllers
{
    public class FirstController:Controller
    {
        private readonly ILogger<FirstController> _logger;
        private readonly ProductService _products;
        public FirstController(ILogger<FirstController> logger,ProductService products)
        {
            _logger=logger;
            _products=products;
        }
        public string Index()
        {
            /*
            this.HttpContext
            this.Request
            this.Respone
            this.RouteDate

            this.User
            this.ModelState
            this.ViewData
            this.ViewBag
            this.Url
            this.TempData
            
            */
           _logger.LogInformation("Index Action");
            return "Toi la controller cua first";
        }
        public IActionResult ChuyenHuongDenHome()
        {
            var url=Url.Action("Privacy","Home");
            return LocalRedirect(url);
        }
        public IActionResult Google()
        {
            return Redirect("https://google.com");
        }
        public IActionResult XinChao()
        {
            return View("/MyView/xinchao1.cshtml");
        }
        public IActionResult HelloView(string username)
        {
            username="2024";
            return View("/MyView/xinchao2.cshtml",username);
        }
        public IActionResult HelloView2(string username)
        {
            username="A nguyen van";
            // HelloView2.cshtml-->/View/First/HelloView2
            // return View();

            // Truyen model
            return View((object)username);
        }
        [TempData]
        public string StatusMessage{set;get;}
        [AcceptVerbs("POST","GET")]
        public IActionResult ProductView(int? id)
        {
            // ProductService _products =new ProductService(); 
            if(id==null)
            {
                
                StatusMessage="Khong tim thay san pham";
                return Redirect(Url.Action("Index","Home"));
            }
            var p = _products.products.Where(p=>p.id==id).FirstOrDefault();
            if(p==null)
            {
                StatusMessage="Khong tim thay san pham";
                return Redirect(Url.Action("Index","Home"));
            }

            return View(p);
        }
        
    }
}