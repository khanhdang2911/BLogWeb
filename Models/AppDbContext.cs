using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CS68_MVC1.Models;

namespace CS68_MVC1
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
                base.OnConfiguring(builder);
        } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PostCategory>(entity=>{
                entity.HasKey(p=>new{p.PostId, p.CategoryId});
            });
        }
        public DbSet<Contact> contacts{set;get;}
        public DbSet<Category> categories{set;get;}
        public DbSet<Post> posts{set;get;}
        public DbSet<PostCategory> postCategories{set;get;} 
    }
}