using Coco.Core.Entities;
using Coco.Core.Interfaces;
using Coco.Core.Models.Request;
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

        public Task<VirtualCart> ApplyVourcher(string code, string nameStore)
        {
            throw new NotImplementedException();
        }

        public Task<VirtualCart> CreateAsync(VirtualCartFilter virtualCartFilter)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
