using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CS68_MVC1.Models
{
    [Table("PostCategory")]
  public class PostCategory
  {
    public int PostId{set;get;}
    public int CategoryId{set;get;}
    public Category Category{set;get;}
    public Post Post{set;get;}
  }
}