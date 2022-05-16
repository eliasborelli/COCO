using Coco.Core.Entities;
using Coco.Infraestructure.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Services.Interfaces
{
    public interface IStockService
    {
        Task<Result<Store>> GetStockByStore(string name);

        Task<Result> Update(Stock stock);
    }
}
