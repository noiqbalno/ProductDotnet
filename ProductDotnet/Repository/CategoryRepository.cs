using Microsoft.EntityFrameworkCore;
using ProductDotnet.Models;
using ProductDotnet.RepositoryContext;

namespace ProductDotnet.Repository
{
    public class CategoryRepository : IRepositoryBase<Category>
    {
        private readonly RepositoryDbContext _context;

        public CategoryRepository(RepositoryDbContext context)
        {
            _context = context;
        }

        public void Create(Category entity)
        {
            _context.Categories.Add(entity);

        }

        public void Delete(Category entity)
        {
            _context.Categories.Remove(entity);
        }

        public async Task<IQueryable<Category>> FindAll(bool trackChanges)
        {
            return !trackChanges ? _context.Categories.AsNoTracking() : _context.Categories;
        }


        public async Task<Category> FindById(int id, bool trackChanges)
        {
            return await _context.Categories.FindAsync(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Category entity)
        {
            _context.Categories.Update(entity);
        }


    }
}