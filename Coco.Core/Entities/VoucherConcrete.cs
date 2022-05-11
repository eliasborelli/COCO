using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Core.Entities
{
    public class VoucherConcrete : BaseEntity
    {
        public virtual VoucherStrategy VoucherStrategy { get; set; }
        public string DiscountStrategy { get; set; }
        public string DiscountProductsOrCategories { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
