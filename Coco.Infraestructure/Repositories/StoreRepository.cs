using Coco.Core.Entities;
using Coco.Core.Interfaces;
using Coco.Infraestructure.Persistence;

namespace Coco.Infraestructure.Repositories
{
    public class StoreRepository : Repository<Store>, IStoreRepository
    {
        public StoreRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
