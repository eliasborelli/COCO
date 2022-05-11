using Coco.Core.Entities;
using Coco.Core.Models.Request;
using Coco.Core.Models.Response;

namespace Coco.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModelResponse>> GetAllAvailableProducts();
        Task<Product> GetProductByFilter(ProductFilter filter);
        Task<IEnumerable<ProductModelResponse>> GetAllProductsByStore(string name);
    }
}
