using Coco.Core.Entities;
using Coco.Core.Models.Response;
using Coco.Infraestructure.Commons;

namespace Coco.Services.Interfaces
{
    public interface IStoreService
    {
        Task<Result<IEnumerable<Store>>> GetStoresByDate(DateTime date);
        Task<Result> Setup();
        Task<Result<IEnumerable<StoreResponse>>> GetSetupData();
    }
}
