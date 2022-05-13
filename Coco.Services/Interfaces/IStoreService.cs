using Coco.Core.Entities;
using Coco.Infraestructure.Commons;

namespace Coco.Services.Interfaces
{
    public interface IStoreService
    {
        Task<Result<IEnumerable<Store>>> GetStoresByDateAsync(DateTime date);
        Task<Result> SetupAsync();
    }
}
