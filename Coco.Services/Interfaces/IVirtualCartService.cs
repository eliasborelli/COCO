using Coco.Core.Entities;
using Coco.Core.Models.Request;
using Coco.Infraestructure.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Services.Interfaces
{
    public interface IVirtualCartService
    {
        Task<Result<VirtualCart>> CreateAsync(VirtualCartFilter virtualCartFilter);
        Task<Result> RemoveAsync(string id);
        Task<Result<VirtualCart>> ApplyVourcher(string code, string nameStore);
    }
}
