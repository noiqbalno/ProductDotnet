using Microsoft.EntityFrameworkCore;
using ProductDotnet.Models;
using ProductDotnet.RepositoryContext;

namespace ProductDotnet.Repository
{
    public class ProductRepository : IRepositoryBase<Product>
    {

        private readonly RepositoryDbContext _repo;

        public ProductRepository(RepositoryDbContext repo)
        {
            _repo = repo;
        }

        public void Create(Product entity)
        {
            _repo.Products.Add(entity);

        }

        public void Delete(Product entity)
        {
            _repo.Products.Remove(entity);
        }

        public async Task<IQueryable<Product>> FindAll(bool trackChanges)
        {
            return !trackChanges ? _repo.Products.Include(x => x.Category).AsNoTracking() : _repo.Products;
        }


        public async Task<Product> FindById(int id, bool trackChanges)
        {
            return await _repo.Products.Include(x => x.Category).FirstOrDefaultAsync(v => v.Id == id);
        }

        public void Save()
        {
            _repo.SaveChanges();
        }

        public void Update(Product entity)
        {
            _repo.Products.Update(entity);
        }
    }
}
