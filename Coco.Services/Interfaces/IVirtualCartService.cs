using Coco.Core.Entities;
using Coco.Core.Models.Request;
using Coco.Core.Models.Response;
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
        Task<Result<VirtualCartResponse>> Create(VirtualCartFilter virtualCartFilter);
        Task<Result> Remove(VirtualCartRemoveFilter virtualCartRemoveFilter);
        Task<Result<VirtualCartResponse>> ApplyVourcher(VoucherFilter voucherFilter);
        Task<Result<VirtualCartResponse>> AddItems(VirtualCartEditFilter virtualCartEditFilter);
        Task<Result<VirtualCartResponse>> RemoveItems(VirtualCartProductsRemoveFilter virtualCartProductsRemoveFilter);
        Task<Result<IEnumerable<VirtualCartResponse>>> GetAll();
    }
}
