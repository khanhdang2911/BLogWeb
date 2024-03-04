using Microsoft.AspNetCore.Mvc;

namespace CS68_MVC1
{
    [Area("Database")]
    [Route("/database-manage/[action]")]
    public class DbManageController:Controller
    {
        private readonly AppDbContext _context;

        public DbManageController(AppDbContext context)
        {
            _context=context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult DeleteDb()
        {
            return View();
        }
        [TempData]
        public string StatusMessage{set;get;}

        [HttpPost]
        public IActionResult DeleteSubmit()
        {

            var success=_context.Database.EnsureDeleted();
            if(success)
            {
                StatusMessage="Xóa database thành công";
            }
            else
            {
                StatusMessage="Xóa không thành công";
            }
            return RedirectToAction("Index");
        }
        public IActionResult CreateDb()
        {
            var success=_context.Database.EnsureCreated();
            if(success)
            {
                StatusMessage="Tạo database thành công";
            }
            else
            {
                StatusMessage="Tạo không thành công";
            }
            return RedirectToAction("Index");
        }
    }
}