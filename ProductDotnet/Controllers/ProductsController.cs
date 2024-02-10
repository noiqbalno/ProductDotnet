using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductDotnet.Models;
using ProductDotnet.Models.Dto;
using ProductDotnet.Repository;
using ProductDotnet.RepositoryContext;
using ProductDotnet.Service;

namespace ProductDotnet.Controllers
{
    public class ProductsController : Controller
    {

        private readonly IProductService<ProductDto> _productService;
        private readonly ICategoryService<CategoryDto> _categoryService;

        private readonly IRepositoryBase<Product> _repositoryBase;
        private readonly IRepositoryBase<Category> _repoCat;


        public ProductsController(IProductService<ProductDto> productService, ICategoryService<CategoryDto> categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var repositoryDbContext = await _productService.FindAll(true);
            return View(repositoryDbContext);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.FindById(id, true);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _categoryService.FindAll(true), "Id", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,Price,Stock,Photo,CategoryId")] ProductDtoCreate productCreate)
        {
            //var cat = _context.Categories.Find(productCreate.CategoryId);
            //var cat = await _categoryService.FindById(productCreate.CategoryId, true);

            if (ModelState.IsValid)
            {
                try
                {
                    var file = productCreate.Photo;
                    var folderName = Path.Combine("Resources", "Images");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = Path.Combine(folderName, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        var productNew = new ProductDto
                        {
                            ProductName = productCreate.ProductName,
                            Stock = productCreate.Stock,
                            Price = productCreate.Price,
                            CategoryId = productCreate.CategoryId,
                            Photo = fileName,
                            //Category = cat
                        };
                        _productService.Create(productNew);

                        ViewData["CategoryId"] = new SelectList(await _categoryService.FindAll(true), "Id", "CategoryName", productCreate.CategoryId);
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }



            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                var errorMessage = error.ErrorMessage;
                Console.WriteLine($"ERror: {errorMessage}");
            }


            return View(productCreate);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int pId = (int)id;

            var product = await _productService.FindById(pId, true);

            var productDtoCreate = new ProductDtoCreate
            {
                Id = product.Id,
                ProductName = product.ProductName,
                CategoryId = product.CategoryId,
                Stock = product.Stock,
                Price = product.Price
            };

            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _categoryService.FindAll(true), "Id", "CategoryName", productDtoCreate.CategoryId);
            return View(productDtoCreate);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,Price,Stock,Photo,CategoryId")] ProductDtoCreate productCreate)
        {
            if (id != productCreate.Id)
            {
                return NotFound();
            }

            //var cat = _context.Categories.Find(productCreate.CategoryId);

            if (ModelState.IsValid)
            {
                try
                {
                    var file = productCreate.Photo;
                    var folderName = Path.Combine("Resources", "Images");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = Path.Combine(folderName, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        //collect data from dto dan filename
                        var categoryDto = new ProductDto
                        {
                            Id = productCreate.Id,
                            ProductName = productCreate.ProductName,
                            Stock = productCreate.Stock,
                            Price = productCreate.Price,
                            CategoryId = productCreate.CategoryId,
                            Photo = fileName,
                            //Category = cat
                        };
                        _productService.Update(categoryDto);

                        return RedirectToAction(nameof(Index));

                    }


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(productCreate.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productCreate);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int pId = (int)id;

            var product = await _productService.FindById(pId, true);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == null)
            {
                return Problem("Entity set 'RepositoryDbContext.Products'  is null.");
            }
            var category = _productService.FindAll(true).Result.FirstOrDefault(m => m.Id == id);
            if (category != null)
            {
                _productService.Delete(category);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (_productService.FindAll(true)?.Result.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
