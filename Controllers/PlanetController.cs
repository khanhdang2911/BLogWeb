using Microsoft.AspNetCore.Mvc;

namespace CS68_MVC1.Controllers
{
    [Route("he-mat-troi/[action]")]
    public class PlanetController:Controller
    {
        private readonly PlanetService _planetService;
        public  PlanetController(PlanetService planetService)
        {
            _planetService=planetService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [BindProperty(SupportsGet =true,Name ="action")]
        public string Name{set;get;}
        public IActionResult Mercury()
        {
            var kq=_planetService.Where(p=>p.name==Name).FirstOrDefault();
            if(kq!=null)
            {
                return View("Detail",kq);
            }
            return NotFound();
        }    

        public IActionResult Venus()
        {
            var kq=_planetService.Where(p=>p.name==Name).FirstOrDefault();
            if(kq!=null)
            {
                return View("Detail",kq);
            }
            return NotFound();
        }  
        public IActionResult Earth()
        {
            var kq=_planetService.Where(p=>p.name==Name).FirstOrDefault();
            if(kq!=null)
            {
                return View("Detail",kq);
            }
            return NotFound();
        }  
        public IActionResult Mars()
        {
            var kq=_planetService.Where(p=>p.name==Name).FirstOrDefault();
            if(kq!=null)
            {
                return View("Detail",kq);
            }
            return NotFound();
        }  
        public IActionResult Jupiter()
        {
            var kq=_planetService.Where(p=>p.name==Name).FirstOrDefault();
            if(kq!=null)
            {
                return View("Detail",kq);
            }
            return NotFound();
        }  
        public IActionResult Saturn()
        {
            var kq=_planetService.Where(p=>p.name==Name).FirstOrDefault();
            if(kq!=null)
            {
                return View("Detail",kq);
            }
            return NotFound();
        }  
        public IActionResult Uranus()
        {
            var kq=_planetService.Where(p=>p.name==Name).FirstOrDefault();
            if(kq!=null)
            {
                return View("Detail",kq);
            }
            return NotFound();
        }  
        [Route("sao/[action]",Order =1,Name ="neptune1")]
        [Route("sao/[controller]/[action]",Order =3,Name ="neptune3")]
        [Route("[controller]-[action]",Order =2,Name ="neptune2")]
        
        public IActionResult Neptune()
        {
            var kq=_planetService.Where(p=>p.name==Name).FirstOrDefault();
            if(kq!=null)
            {
                return View("Detail",kq);
            }
            return NotFound();
        }  
        [Route("hanhtinh/{id:int}")]
        public IActionResult PlanetInfo(int? id)
        {   
            if(id==null)
                return Content("Khong tim thay hanh tinh");
            
            var kq= _planetService.Where(p=>p.id==id).FirstOrDefault();
            if(kq!=null)
            {
                return View("Detail",kq);
            }
            return Content("Khong tim thay hanh tinh voi id ban dang tim");
            
        }
    }
}