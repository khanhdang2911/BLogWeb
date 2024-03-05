// using System.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Đảm bảo đã import namespace này
using System.Linq;
using CS68_MVC1.Models;
using Microsoft.AspNetCore.Authorization;
namespace CS68_MVC1.Controllers
{
    [Authorize(Roles="Admin")]
    [Area("CategoryManage")]
    [Route("/View-post/[action]")]
    public class ViewPostController:Controller
    {
        private readonly AppDbContext _context;
        public ViewPostController(AppDbContext context)
        {
            _context=context;
        }
        public void Load(Category category,List<int> postListId)
        {
            _context.Entry(category).Collection(p=>p.CategoryChildren).Load();
            _context.Entry(category).Collection(p=>p.PostCategories).Load();
            if(category.PostCategories!=null)
            {
                foreach(var item in category.PostCategories)
                {
                    if(postListId.Contains(item.PostId)==false)
                        postListId.Add(item.PostId);
                }
            }
            if(category.CategoryChildren!=null)
            {
                foreach(var item in category.CategoryChildren)
                {            
                    Load(item,postListId);
                }
            }
        }
        public IActionResult Index([FromQuery(Name="id")]int? id,[FromQuery(Name="page")]int page=1)
        {
           if(id==null||id==0)
           {
                var allPosts=(from p in _context.posts select p).ToList();
                //So luong post da skip 3 cai
                ViewData["AllPost"]=allPosts.Skip((page-1)*3).Take(3).ToList();
                //So luong post tổng
                ViewData["TotalPost"]=allPosts;
                ViewData["Id"]=0;
                //Trang hien tai
                ViewData["pageHienTai"]=page;
                return View();
           }
            var kq=_context.categories.Where(c=>c.Id==id);
            if(kq==null)
                return NotFound();
            Category category=kq.FirstOrDefault();
            List<int>postListId=new List<int>();
            Load(category,postListId);
            
            List<Post> Allposts=new List<Post>();
            foreach(var postId in postListId)
            {
                var post=_context.posts.Where(p=>p.Id==postId).FirstOrDefault();
                Allposts.Add(post);
            }
            var allPostQr=Allposts.AsQueryable().Skip((page-1)*3).Take(3);
            ViewData["Id"]=id;
            //So luong post đã skip 3 cái
            ViewData["AllPost"]=allPostQr.ToList();
            ViewData["TotalPost"]=Allposts;//Số lượng post tổng cộng của category
            return View(category);
        }
    }
}