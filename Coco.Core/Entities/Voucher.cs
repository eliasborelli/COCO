using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Core.Entities
{
    public class Voucher : BaseEntity
    {
        public virtual Store Store { get; set; }
        public string Code { get; set; }
        public virtual VoucherConcrete VoucherConcrete { get; set; }
    }
}
