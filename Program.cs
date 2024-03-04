using CS68_MVC1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<PlanetService>();
// builder.Services.AddSingleton<AppDbContext>();
builder.Services.AddDbContext<AppDbContext>(options =>
        {
            string connectString = builder.Configuration.GetConnectionString("MyBlogContext");
            options.UseSqlServer(connectString);
        });
var app = builder.Build();
// builder.Services.AddRazorPages();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();//identity
app.UseAuthorization();//quyen truy cap


app.MapControllerRoute(
        name: "first",
        pattern: "{url}/{id?}",
        defaults: new{
            controller= "First",
            action="ProductView",
        },
        constraints: new{
            url="xemsanpham",
            id=new RangeRouteConstraint(2,4)
        }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
// app.MapRazorPages();
// app.MapControllerRoute(
//     name: "firstroute",
//     pattern: "start-here",
//     defaults :new{
//         controller= "First",
//         action="ProductView",
//         id=3
//     }
// );
app.MapControllerRoute(
    name: "firstroute",
    pattern: "start-here/{controller}/{action}/{id?}"
);
app.MapAreaControllerRoute(
    name: "product",
    areaName: "ProductManage",
    pattern:"{controller}/{action=Index}"
);

app.MapAreaControllerRoute(
    name: "blog",
    areaName: "CategoryManage",
    pattern:"{controller}/{action=Index}"
);

//Cac attribute

// [AcceptVerbs]
    /*
    Vi du [AcceptVerbs("POST")] la chi duoc truy cap bang phuong thuc Post
    */
// [Route]
// [HttpGet]
// [HttpPost]
// [HttpPut]
// [HttpDelete]
// [HttpHead]
// [HttpPatch]



app.Run();
