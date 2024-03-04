using Microsoft.AspNetCore.Mvc;

namespace CS68_MVC1.Components
{
    [ViewComponent]
    public class CategorySidebar:ViewComponent
    {
        private readonly AppDbContext _context;
        public CategorySidebar(AppDbContext context)
        {
            _context=context;
        }
        public  IViewComponentResult Invoke()
        {
            var kq=(from p in _context.categories where p.ParentCategoryId==null select p).ToList();
            foreach(var item in kq)
            {
                _context.Entry(item).Collection(i=>i.CategoryChildren).Load();
            }
            return View(kq);
        }
    }
}