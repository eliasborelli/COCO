using Coco.Core.Entities;
using Coco.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Services.Services
{
    public class StoreService : IStoreService
    {
        public IEnumerable<Store> GetStoresByDate(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
