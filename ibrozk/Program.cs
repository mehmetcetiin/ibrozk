using ibrozk.Data.Abstract;
using ibrozk.Data.Concrete;
using ibrozk.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddDbContext<ImageContext>(options =>
{
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("sql_connection");
    options.UseSqlite(connectionString);
});

builder.Services.AddScoped<IPostRepository, EfPostRepository>();
builder.Services.AddScoped<ITagRepository, EfTagRepository>();
builder.Services.AddScoped<IUserRepository, EfUserRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

var app = builder.Build();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

SeedData.TestVerileriniDoldur(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.MapControllerRoute(
    name: "deleteConfirmed",
    pattern: "images/DeleteConfirmed/{id?}", // veya başka bir URL şeması
    defaults: new { controller = "Images", action = "DeleteConfirmed" }
);

app.MapControllerRoute(
    name: "create",
    pattern: "images/create",
    defaults: new { controller = "Images", Action = "Create" }
    );

app.MapControllerRoute(
    name: "createbyList",
    pattern: "images/createbylist",
    defaults: new { controller = "Images", Action = "CreatebyList" }
    );

app.MapControllerRoute(
    name: "list",
    pattern: "images/list",
    defaults: new { controller = "Images", Action = "List" }
    );

app.MapControllerRoute(
    name: "posts_by_tag",
    pattern: "images/{tag}",
    defaults: new { controller = "Images", Action = "Index" }
    );


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Images}/{action=Index}/{id?}"
    );



app.Run();
