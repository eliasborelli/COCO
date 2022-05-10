using Coco.Core.Entities;
using Coco.Core.Models.Request;
using Coco.Core.Models.Response;

namespace Coco.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductModelResponse> GetAllProducts();
        Product GetProductByFilter(ProductFilter filter);
        IEnumerable<Product> GetAllProductsByStore(string id);
    }
}
