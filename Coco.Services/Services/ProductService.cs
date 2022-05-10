using Coco.Core.Entities;
using Coco.Core.Models.Request;
using Coco.Core.Models.Response;
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
        public IEnumerable<ProductModelResponse> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAllProductsByStore(string id)
        {
            throw new NotImplementedException();
        }

        public Product GetProductByFilter(ProductFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
