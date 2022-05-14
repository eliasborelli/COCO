using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Core.Models.Response
{
    public class VoucherConcreteResponse
    {
        public Guid VoucherConcreteId { get; set; }
        public virtual VoucherStrategyResponse VoucherStrategy { get; set; }
        public string DiscountStrategy { get; set; }
        public string DiscountProductsOrCategories { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
