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
    public class CategoriesController : Controller
    {
        private readonly RepositoryDbContext _context;

        private readonly ICategoryService<CategoryDto> _categoryService;

        //replace RepositoryDbContext with IRepositoryBase
        private readonly IRepositoryBase<Category> _repositoryBase;


        public CategoriesController(ICategoryService<CategoryDto> categoryService)
        {
            _categoryService = categoryService;
        }


        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.FindAll(true));

        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryService.FindAll(true).Result.FirstOrDefault(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryName,Description,Photo")] CategoryDtoCreate categoryDtoCreate)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var file = categoryDtoCreate.Photo;
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
                        var categoryDto = new CategoryDto
                        {
                            CategoryName = categoryDtoCreate.CategoryName,
                            Description = categoryDtoCreate.Description,
                            Photo = fileName
                        };
                        _categoryService.Create(categoryDto);

                        return RedirectToAction(nameof(Index));

                    }
                }
                catch (Exception)
                {

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDtoCreate);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryService.FindById((int)id, true);
            var categoryDtoCreate = new CategoryDtoCreate
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                Description = category.Description
            };

            if (category == null)
            {
                return NotFound();
            }
            return View(categoryDtoCreate);

        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryName,Description,Photo")] CategoryDtoCreate categoryDtoCreate)
        {
            if (id != categoryDtoCreate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var file = categoryDtoCreate.Photo;
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
                        var categoryDto = new CategoryDto
                        {
                            Id = categoryDtoCreate.Id,
                            CategoryName = categoryDtoCreate.CategoryName,
                            Description = categoryDtoCreate.Description,
                            Photo = fileName
                        };
                        _categoryService.Update(categoryDto);

                        return RedirectToAction(nameof(Index));

                    }


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(categoryDtoCreate.Id))
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
            return View(categoryDtoCreate);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var category = await _categoryService.FindById((int)id,true);

            var category = _categoryService.FindAll(true).Result.FirstOrDefault(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return Problem("Entity set 'RepositoryDbContext.Categories'  is null.");
            }
            //var category = await _categoryService.FindById((int)id,true);
            var category = _categoryService.FindAll(true).Result.FirstOrDefault(m => m.Id == id);
            if (category != null)
            {
                _categoryService.Delete(category);
            }

            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return (_categoryService.FindAll(true)?.Result.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
