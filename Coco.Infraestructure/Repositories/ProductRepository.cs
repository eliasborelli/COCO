using Coco.Core.Entities;
using Coco.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Infraestructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public IEnumerable<Product> GetAllProductsByStore(string id)
        {
            throw new NotImplementedException();
        }

        public Product GetProductByFilter(string Code, string Description, string Store)
        {
            throw new NotImplementedException();
        }
    }
}
