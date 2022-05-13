using Coco.Core.Entities;
using Coco.Core.Interfaces;
using Coco.Core.Models.Request;
using Coco.Core.Models.Response;
using Coco.Infraestructure.Commons;
using Coco.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IRepository<Stock> _stockRepository;
        private readonly IRepository<Store> _storeRepository;
        public ProductService(IProductRepository productRepository, IRepository<Stock> stockRepository, IRepository<Store> storeRepository)
        {
            _productRepository = productRepository;
            _stockRepository = stockRepository;
            _storeRepository = storeRepository;
        }

        public async Task<Result<IEnumerable<ProductModelResponse>>> GetAllAvailableProducts()
        {
            var stock = await _stockRepository.GetAsync();

            var response = stock.GroupBy(prod => new { prod.Product.Description })
                .Select(prod => new ProductModelResponse()
                {
                    Description = prod.First().Product.Description,
                    TotalStock = prod.Sum(x => x.CurrentStock),
                    Amount = prod.First().Product.Amount
                }).ToList();

            return Result.Success<IEnumerable<ProductModelResponse>>(response);
        }

        public async Task<Result<IEnumerable<ProductModelResponse>>> GetAllProductsByStore(string name)
        {
            var store = await _storeRepository.GetFirstAsync(x => x.Name.Contains(name));

            return Result.Success<IEnumerable<ProductModelResponse>>(store.Stocks.Select(prod => new ProductModelResponse()
            {
                Description = prod.Product.Description,
                Amount = prod.Product.Amount,
                TotalStock = prod.CurrentStock
            }));
        }

        public async Task<Result<Product>> GetProductByFilter(ProductFilter filter)
        {
            Product product = new Product();

            var store = await _storeRepository.GetFirstAsync(x => x.Name.Contains(filter.store));

            var stock = store.Stocks.FirstOrDefault(prod => prod.Product.Description.Contains(filter.Description));

            return Result.Success<Product>(product);
        }
    }
}
