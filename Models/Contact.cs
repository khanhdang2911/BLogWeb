using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace CS68_MVC1.Models
{
    public class Contact
    {
        [Key]
        [Required]
        public int id{set;get;}
        [Required]
        public string name{get;set;}
        [EmailAddress]
        public string email{set;get;}
        public DateTime? DateSent{set;get;}
        public string content{set;get;}
        
    }
}