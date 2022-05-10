using Coco.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Core.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Product GetProductByFilter(string Code, string Description, string Store);
        IEnumerable<Product> GetAllProductsByStore(string id);
    }
}
