using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CS68_MVC1.Models
{
    [Table("Post")]
  public class Post
  {

      [Key]
      public int Id { get; set; }


      [Required(ErrorMessage = "Phải có tên danh mục")]
      [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} dài {2} đến {1}")]
      [Display(Name = "Tên danh mục")]
      public string Title { get; set; }

      [DataType(DataType.Text)]
      [Display(Name = "Nội dung danh mục")]
      public string Content { set; get; }
      public DateTime DateCreated{set;get;}

    
      public ICollection<PostCategory>? PostCategories { get; set; }


  }
}