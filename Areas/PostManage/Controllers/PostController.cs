using Aspose.Words;
using Aspose.Words.Saving;
using CS68_MVC1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace CS68_MVC1.PostManage
{
    [Area("PostManage")]
    [Route("/PostManage-Post/[action]")]
    public class PostController:Controller
    {
        private readonly AppDbContext _context;
        public PostController(AppDbContext context)
        {
            _context=context;
        }
        public IActionResult Index()
        {
            var kq=from p in _context.posts select p;
            return View(kq.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["ListCategory"]=new MultiSelectList(_context.categories,"Id","Title");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Title,Content,categorylistID")] CategoryListOfPost post)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            post.DateCreated=DateTime.Now;
            await _context.posts.AddAsync(post);
            await _context.SaveChangesAsync();
            foreach(var item in post.categorylistID)
            {
                PostCategory postCategory=new PostCategory()
                {
                    PostId=post.Id,
                    CategoryId=item
                };
                await _context.postCategories.AddAsync(postCategory);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            return View((object)id);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            if(id==null)
            {
                return NotFound(); 
            }
            var kq=_context.posts.Where(p=>p.Id==id).FirstOrDefault();
            if(kq==null)
            {
                return NotFound();
            }
            _context.posts.Remove(kq);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
            
        }
        public IActionResult Details(int id)
        {
            if(id==null)
            {   
                return NotFound();
            }
            var kq=_context.posts.Where(p=>p.Id==id).FirstOrDefault();
            if(kq==null)
            {
                return NotFound();
            }
            return View(kq);
        }
        public IActionResult Edit(int id)
        {
            if(id==null)
                return NotFound();
            
            ViewData["ListCategory"]=new MultiSelectList(_context.categories,"Id","Title");
            ViewData["id"]=id;
            var kq=_context.posts.Where(p=>p.Id==id).FirstOrDefault();
            
            if(kq==null)
                return NotFound();

            CategoryListOfPost postL=new CategoryListOfPost()
            {
                Id=kq.Id,
                Title=kq.Title,
                Content=kq.Content
            };
            return View(postL);
        }
        [HttpPost]
        public async Task<IActionResult>Edit([Bind("Title,Content,categorylistID")] CategoryListOfPost post,int id)
        {
             if(!ModelState.IsValid)
            {
                return View();
            }
            var kq=_context.posts.Where(p=>p.Id==id).FirstOrDefault();
            if(kq==null)
            {
                return Content("Khong tim thay trang de edit");
            }
            //Chinh sua table post
            _context.Entry(kq).State=EntityState.Modified;
            kq.Title=post.Title;
            kq.Content=post.Content;
            await _context.SaveChangesAsync();
            
            var catepost=(from i in _context.postCategories
                         where i.PostId==kq.Id select i.CategoryId).ToList();
            //Chinh sua table PostCategory
            
            //them
            foreach(var item in post.categorylistID)
            {
                if(catepost.Contains(item)==false)
                {
                   PostCategory addPostCategory=new PostCategory()
                   {
                    PostId=kq.Id,
                    CategoryId=item
                   };
                    _context.postCategories.Add(addPostCategory);
                    await _context.SaveChangesAsync();
                }
            }
            //xoa
            var deletePostCategory=(from p in _context.postCategories
                                    where p.PostId==kq.Id && post.categorylistID.Contains(p.CategoryId)==false select p).ToList();
            _context.postCategories.RemoveRange(deletePostCategory);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }   
    }
}