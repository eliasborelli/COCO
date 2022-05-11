using Coco.Core.Entities;
using Coco.Core.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Services.Interfaces
{
    public interface IVirtualCartService
    {
        Task<VirtualCart> CreateAsync(VirtualCartFilter virtualCartFilter);
        Task RemoveAsync(string id);
        Task<VirtualCart> ApplyVourcher(string code, string nameStore);
    }
}
