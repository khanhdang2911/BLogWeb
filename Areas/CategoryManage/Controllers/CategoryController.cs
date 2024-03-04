using CS68_MVC1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
namespace CS68_MVC1.Controllers
{
    [Area("CategoryManage")]
    [Route("/catgory-manage/[action]")]
    public class CategoryController:Controller
    {
        private readonly AppDbContext _context;
        public CategoryController (AppDbContext context)
        {
            _context=context;
        }
        public IActionResult Index()
        {
            var kq=(from c in _context.categories
                where c.ParentCategoryId==null select c).Include(c=>c.CategoryChildren).ToList();

            return View(kq);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var kq=(from c in _context.categories select c).ToList();
            kq.Add(
                new Category(){
                    Id=-1,
                    Title="Không có danh mục cha"
                }
            );
            ViewData["ParentCategoryId"]=new SelectList(kq,"Id","Title");
    
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Title,Content,ParentCategoryId")] Category category)
        {
            if(category.ParentCategoryId==-1)
            {
                category.ParentCategoryId=null;
            }
            if(!ModelState.IsValid)
            {
                return View();
            }
            await _context.categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            return View((object)id);   
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            
            if(id==null)
            {
                return Content("khong tim thay");
            }
            var kq=_context.categories.Where(c=>c.Id==id).FirstOrDefault();
            
            if(kq==null)
            {
                return Content("Khong tim thay");
            }
            await _context.Entry(kq).Collection(c=>c.CategoryChildren).LoadAsync();
            if(kq.CategoryChildren!=null)
            {
                foreach(var item in kq.CategoryChildren)
                {
                    item.ParentCategoryId=kq.ParentCategoryId;
                    _context.Entry(item).State=EntityState.Modified;
                    await _context.SaveChangesAsync();
                }   
            }
            _context.categories.Remove(kq);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            if(id==null)
            {
                return Content("khong tim thay");
            }
            var kq=_context.categories.Where(c=>c.Id==id).FirstOrDefault();
            if(kq==null)
            {
                return Content("Khong tim thay");
            }

            return View(kq);
        }
        public IActionResult Edit(int id)
        {
            var categorilist=(from c in _context.categories select c).ToList();
            categorilist.Add(
                new Category(){
                    Id=-1,
                    Title="Không có danh mục cha"
                }
            );
            var cateNotSelected1=categorilist.Where(ct=>ct.Id==id).FirstOrDefault();
            categorilist.Remove(cateNotSelected1);
            
            var kq=_context.categories.Where(c=>c.Id==id).FirstOrDefault();
            _context.Entry(kq).Collection(c=>c.CategoryChildren).Load();
            foreach(var item in kq.CategoryChildren)
            {
                categorilist.Remove(item);
            }

            ViewData["ParentCategoryId"]=new SelectList(categorilist,"Id","Title");
            return View(kq);
        }
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Title,Content,ParentCategoryId")] Category category,int id)
        {
            if(category.ParentCategoryId==-1)
            {
                category.ParentCategoryId=null;
            }
            if(!ModelState.IsValid)
            {
                return View();
            }
            var kq=_context.categories.Where(c=>c.Id==id).FirstOrDefault();
            _context.Entry(kq).State= EntityState.Modified;
            kq.Title=category.Title;
            kq.Content=category.Content;
            kq.ParentCategory=category.ParentCategory;
            kq.CategoryChildren=category.CategoryChildren;
            kq.ParentCategoryId=category.ParentCategoryId;
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult ViewBlog()
        {
            
            return View();
        }
    }
}