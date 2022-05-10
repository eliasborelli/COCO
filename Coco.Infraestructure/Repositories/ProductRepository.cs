using Coco.Core.Entities;
using Coco.Core.Interfaces;
using Coco.Infraestructure.Persistence;

namespace Coco.Infraestructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {

        }

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
