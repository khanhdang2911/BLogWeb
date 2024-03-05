using System.Security.Claims;
using CS68_MVC1.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CS68_MVC1.Models
{
   public class RoleClaim
   {
        public int UserID{set;get;}
        public string ClaimTypes{set;get;}
        public string ClaimName{set;get;}
        public UserModel? User {set;get;}
   }
}