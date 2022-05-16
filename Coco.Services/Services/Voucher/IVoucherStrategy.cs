using Coco.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Services.Services.Voucher
{
    public interface IVoucherStrategy
    {
        VirtualCart Execute(VoucherConcrete voucherConcrete, VirtualCart virtualCart);
    }
}
