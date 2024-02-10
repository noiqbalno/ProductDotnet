using AutoMapper;
using Microsoft.CodeAnalysis;
using ProductDotnet.Models;
using ProductDotnet.Models.Dto;
using ProductDotnet.Repository;

namespace ProductDotnet.Service
{
    public class ProductService : IProductService<ProductDto>
    {
        private readonly IRepositoryBase<Product> _repositoryBase;
        private readonly IMapper _mapper;

        public ProductService(IRepositoryBase<Product> repositoryBase, IMapper mapper)
        {
            _repositoryBase = repositoryBase;
            _mapper = mapper;
        }

        public void Create(ProductDto entity)
        {
            //var product = _mapper.Map<Product>(entity);
            var product = new Product
            {
                //Category = entity.Category,
                ProductName = entity.ProductName,
                Photo = entity.Photo,
                Price = entity.Price,
                Stock = entity.Stock,
                CategoryId = entity.CategoryId,
            };

            _repositoryBase.Create(product);
            _repositoryBase.Save();
        }

        public void Delete(ProductDto entity)
        {
            var product = _mapper.Map<Product>(entity);
            _repositoryBase.Delete(product);
            _repositoryBase.Save();
        }


        public async Task<IEnumerable<ProductDto>> FindAll(bool trackChanges)
        {
            var products = await _repositoryBase.FindAll(false);
            var productDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return productDto;
        }

        public async Task<ProductDto> FindById(int id, bool trackChanges)
        {
            var product = await _repositoryBase.FindById(id, false);
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

        public async void Update(ProductDto entity)
        {
            var product = _mapper.Map<Product>(entity);
            //var product = await _repositoryBase.FindById(entity.Id, true);

            //product.ProductName = entity.ProductName;
            //product.Photo = entity.Photo;
            //product.Price = entity.Price;
            //product.Stock = entity.Stock;
            //product.CategoryId = entity.CategoryId;

            _repositoryBase.Update(product);
            _repositoryBase.Save();
        }
    }
}
