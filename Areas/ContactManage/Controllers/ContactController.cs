using Microsoft.EntityFrameworkCore;
// using System.Data.Entity;
using CS68_MVC1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CS68_MVC1.ContactManage
{
    [Area("ContactManage")]
    [Route("/contact-manage/[action]")]
    public class ContactController:Controller
    {
        private readonly AppDbContext _DbContext;
        public ContactController(AppDbContext DbContext)
        {
            _DbContext=DbContext;
            
        }
        
        [TempData]
        public string StatusMessage{set;get;}
        public IActionResult Index()
        {
            var kq=(from c in _DbContext.contacts select c).ToList();
            
            return View(kq);
        }
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([Bind("name,email,content")] Contact contact)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            contact.DateSent=DateTime.Now;
            await _DbContext.AddAsync(contact);
            var kq=await _DbContext.SaveChangesAsync();
            StatusMessage="Gửi bải thành công";
            return RedirectToAction("Index","Home");
        }
        [Route("/contact-manage/details/{id}")]
        public IActionResult Details(int id)
        {
            if(id==null)
            {
                return Content("Không tìm thấy trang");
            }
            Contact contact=_DbContext.contacts.Where(c=>c.id==id).FirstOrDefault();
            return View(contact);
        }
        
        // [Route("/contact-manage/delete/{id}")]
        public IActionResult Delete(int id)
        {
            if(id==null)
            {
                return Content("Không tìm thấy trang");
            }
            return View((object)id);
        }
        [HttpPost]
        // [Route("/contact-manage/delete-confirm/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if(id==null)
            {
                return Content("Không tìm thấy trang");
            }
            var contact=await _DbContext.contacts.Where(c=>c.id==id).FirstOrDefaultAsync();
            if(contact!=null)
            {
                _DbContext.Remove(contact);
                await _DbContext.SaveChangesAsync();
                StatusMessage="Xóa thành công";
                return RedirectToAction("Index");
            }
            return View();

        } 
    }
}