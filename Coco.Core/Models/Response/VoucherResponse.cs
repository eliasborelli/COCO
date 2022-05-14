using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Core.Models.Response
{
    public class VoucherResponse
    {
        public Guid VoucherId { get; set; }
        public string Code { get; set; }
        public VoucherConcreteResponse VoucherConcrete { get; set; }
    }
}
