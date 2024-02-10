using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ProductDotnet.Models;
using ProductDotnet.Models.Dto;
using ProductDotnet.Repository;
using ProductDotnet.RepositoryContext;
using ProductDotnet.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<RepositoryDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DbConnection"]);
});

builder.Services.AddScoped<IRepositoryBase<Category>, CategoryRepository>();
builder.Services.AddScoped<ICategoryService<CategoryDto>, CategoryService>();

builder.Services.AddScoped<IRepositoryBase<Product>, ProductRepository>();
builder.Services.AddScoped<IProductService<ProductDto>, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
// set folder resources to static file
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
