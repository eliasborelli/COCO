using Coco.Core.Entities;
using Coco.Core.Interfaces;
using Coco.Core.Models.Request;
using Coco.Core.Models.Response;
using Coco.Infraestructure.Commons;
using Coco.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<Result> Exists(string code, string name)
        {
            if (string.IsNullOrEmpty(code) && string.IsNullOrEmpty(name))
                return Result.Failed();

            Expression<Func<Product, bool>>? conditions = null;

            conditions = (string.IsNullOrEmpty(code) is false) ? (u => u.Code.Trim() == code.Trim()) : (string.IsNullOrEmpty(name) is false) ? (u => u.Description.Trim().Contains(name.Trim())) : null;

            var product = await _productRepository.GetFirstAsync(conditions);

            if (product == null)
                return Result.Failed();

            return Result.Success();
        }

        public async Task<Result<IEnumerable<ProductModelResponse>>> GetAllAvailableProducts()
        {
            var stock = await _stockRepository.GetAsync();

            var response = stock.Where(x => x.CurrentStock > 0).GroupBy(prod => new { prod.Product.Description })
                .Select(prod => new ProductModelResponse()
                {
                    Code = prod.First().Product.Code,
                    Description = prod.First().Product.Description,
                    TotalStock = prod.Sum(x => x.CurrentStock),
                    Amount = prod.First().Product.Amount
                }).ToList();

            return Result.Success<IEnumerable<ProductModelResponse>>(response);
        }

        public async Task<Result<IEnumerable<ProductModelResponse>>> GetAllProductsByStore(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Result.Failed<IEnumerable<ProductModelResponse>>("The store name is required");
            
            var store = await _storeRepository.GetFirstAsync(x => x.Name.Trim().Contains(name.Trim()));

            if (store == null)
                return Result.Failed<IEnumerable<ProductModelResponse>>("The store is not exists");

            return Result.Success<IEnumerable<ProductModelResponse>>(store.Stocks.Where(x => x.CurrentStock > 0).Select(prod => new ProductModelResponse()
            {
                Code = prod.Product.Code,
                Description = prod.Product.Description,
                Amount = prod.Product.Amount,
                TotalStock = prod.CurrentStock
            }));
        }

        public async Task<Result<Product>> GetProductByFilter(ProductFilter filter)
        {
            if (string.IsNullOrEmpty(filter.Code) && string.IsNullOrEmpty(filter.ProductDescription))
                return Result.Failed<Product>("The code or description product is required.");

            Expression<Func<Product, bool>>? conditions = null;

            conditions = (string.IsNullOrEmpty(filter.Code) is false) ? (u => u.Code.Trim() == filter.Code.Trim()) : (string.IsNullOrEmpty(filter.ProductDescription) is false) ? (u => u.Description.Trim().Contains(filter.ProductDescription.Trim())) : null;

            var product = await _productRepository.GetFirstAsync(conditions);

            if (product == null)
                return Result.Failed<Product>("The product is not exists");

            var store = await _storeRepository.GetFirstAsync(x => x.Name.Trim().Contains(filter.StoreName.Trim()));

            if (store == null)
                return Result.Failed<Product>("The store is not exists");

            var response = store.Stocks.FirstOrDefault(x => x.ProductId == product.Id && x.CurrentStock > 0);

            if (response == null)
                return Result.Failed<Product>("Product not available");

            return Result.Success<Product>(product);
        }
    }
}
