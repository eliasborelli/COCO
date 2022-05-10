using Coco.Core.Entities;

namespace Coco.Services.Interfaces
{
    public interface IStoreService
    {
        Task<IEnumerable<Store>> GetStoresByDateAsync(DateTime date);
        Task SetupAsync();
    }
}
