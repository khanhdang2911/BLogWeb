using System.ComponentModel.DataAnnotations;
using CS68_MVC1.Controllers;

namespace CS68_MVC1.Models
{
    public class UserModel
    {
        [Key]
        public int Id{set;get;}
        [Display(Name ="Username")]
        public string UserName{set;get;}

        [Display(Name ="Mật khẩu")]

        public string PassWord{set;get;}
        public ICollection<RoleClaim>? RoleClaims {set;get;} 

    }
}