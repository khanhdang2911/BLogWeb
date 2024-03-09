using System.Security.Claims;
using CS68_MVC1.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace CS68_MVC1.Controllers
{
    public class LoginController:Controller
    {

        public static int _randomCode{set;get;}
        public static string usernameEnter{set;get;}
        private readonly AppDbContext _context;
        
        public LoginController(AppDbContext context)
        {
            _context=context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register([Bind("UserName,PassWord")] UserModel user)
        {  
            if(!ModelState.IsValid)
            {
                Console.WriteLine("SAdas");
                Console.WriteLine(user.UserName +" 123 "+user.PassWord);
                return View();
            }
            _context.users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("DangNhap","Login");
        }
        [HttpGet]
        public IActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DangNhap([Bind("UserName,PassWord")] UserModel user)
        {
            if(!ModelState.IsValid)
            {
                Console.WriteLine(user.UserName +" 123 "+user.PassWord);

                return View();
            }
            var kq=_context.users.Where(u=>u.UserName==user.UserName).FirstOrDefault();
            if(kq==null)
            {
                ModelState.AddModelError("","Sai thông tin đăng nhập");
                return View();
            }
            if(kq!=null)
            {
                if(kq.PassWord!=user.PassWord)
                {
                    ModelState.AddModelError("","Sai mật khẩu");
                    return View();
                }
            }

            List<Claim> claims=new List<Claim>();
            
            var CheckRoleNameForUser=(from u in _context.users join r in _context.roleClaims on kq.Id equals r.UserID select r).ToList();
            if(CheckRoleNameForUser.Count==0)
            {
                claims.Add(new Claim("Name",user.UserName));
                claims.Add(new Claim(ClaimTypes.Role,"Customer"));
                RoleClaim roleClaim=new RoleClaim();

                roleClaim.UserID=kq.Id;
                roleClaim.ClaimTypes="Name";
                roleClaim.ClaimName=user.UserName;

                _context.roleClaims.Add(roleClaim);
                _context.SaveChanges();
                RoleClaim roleClaim2=new RoleClaim();

                roleClaim2.UserID=kq.Id;
                roleClaim2.ClaimTypes=ClaimTypes.Role;
                roleClaim2.ClaimName="Customer";

                _context.roleClaims.Add(roleClaim2);
                _context.SaveChanges();

            }
            else
            {
                foreach(var item in CheckRoleNameForUser)
                {
                    claims.Add(new Claim(item.ClaimTypes,item.ClaimName));
                }
            }
            
            
            var claimIdentity=new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal=new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync(claimPrincipal);
            return RedirectToAction("Index","Home");
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index","Home");
        }
        public IActionResult Forbidden()
        {
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPasswordConfirmCode(string code)
        {
            if(string.IsNullOrEmpty(code))
            {
                ModelState.AddModelError("","Phải nhập code để xác nhận");
                return View();
            }
            

            else if(int.Parse(code)!=_randomCode)
            {
                Console.WriteLine(code+"  "+_randomCode);
                ModelState.AddModelError("","Code bạn nhập không đúng, vui lòng kiểm tra lại");
                return View();
            }
            var user=_context.users.Where(u=>u.UserName==usernameEnter).FirstOrDefault();
            user.PassWord="123456789qwe";
            _context.SaveChangesAsync();
            return RedirectToAction("DangNhap");
        }
        [HttpPost]
        public IActionResult ForgotPasswordEnter(string? username)
        {
            if(string.IsNullOrEmpty(username))
            {
                ModelState.AddModelError("","Phải nhập username của bạn");
                return RedirectToAction("ForgotPassword");
            }
            // Console.WriteLine(username);
            var user=_context.users.Where(u=>u.UserName==username).FirstOrDefault();
            if(user==null)
            {
            Console.WriteLine("sai user");
                ModelState.AddModelError("","Username không tồn tại");
                return RedirectToAction("ForgotPassword");
            }
            usernameEnter=username;
            Random rnd = new Random();
            int randomCode=rnd.Next(100000,999999);
            _randomCode=randomCode;
             var email = new MimeMessage();

            email.From.Add(new MailboxAddress("khanhdang", "khanhdang3152@gmail.com"));
            email.To.Add(new MailboxAddress("thangdang", "khanhdangco2911@gmail.com"));

            email.Subject = "Testing out email sending";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { 
                Text = $"<b>Ma code cua ban la {_randomCode}</b>"};
            Console.WriteLine("Ma code:"+_randomCode);

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, false);

                // Note: only needed if the SMTP server requires authentication
                smtp.Authenticate("khanhdang3152@gmail.com", "tlol kfvg mubs yyyl");

                smtp.Send(email);
                smtp.Disconnect(true);
            }
            Console.WriteLine("da toi buoc nay roi nay");
            
           

            return View("ForgotPasswordConfirmCode",(object)randomCode);
        }
        
    }
}