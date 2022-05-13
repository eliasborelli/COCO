using Coco.Core.Entities;
using Coco.Core.Models.Request;
using Coco.Core.Models.Response;
using Coco.Infraestructure.Commons;

namespace Coco.Services.Interfaces
{
    public interface IProductService
    {
        Task<Result<IEnumerable<ProductModelResponse>>> GetAllAvailableProducts();
        Task<Result<Product>> GetProductByFilter(ProductFilter filter);
        Task<Result<IEnumerable<ProductModelResponse>>> GetAllProductsByStore(string name);
    }
}
