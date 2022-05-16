using Coco.Core.Entities;
using Coco.Infraestructure.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Services.Interfaces
{
    public interface IVoucherService
    {
        Task<Result<Voucher>> GetVoucherByCode(string code);
    }
}
