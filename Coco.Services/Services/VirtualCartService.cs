using Coco.Core.Entities;
using Coco.Core.Interfaces;
using Coco.Core.Models.Request;
using Coco.Infraestructure.Commons;
using Coco.Services.Interfaces;

namespace Coco.Services.Services
{
    public class VirtualCartService : IVirtualCartService
    {
        private readonly IRepository<VirtualCart> _virtualCartRepository;
        public VirtualCartService(IRepository<VirtualCart> virtualCartRepository)
        {
            _virtualCartRepository = virtualCartRepository;
        }

        public async Task<Result<VirtualCart>> ApplyVourcher(string code, string nameStore)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<VirtualCart>> CreateAsync(VirtualCartFilter virtualCartFilter)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> RemoveAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
