using Coco.Core.Entities;

namespace Coco.Services.Interfaces
{
    public interface IStoreService
    {
        IEnumerable<Store> GetStoresByDate(DateTime date);
    }
}
