using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Core.Entities
{
    public class VoucherStrategy : BaseEntity
    {
        public Guid VoucherConcreteId { get; set; }
        public string CodeStrategy { get; set; }
        public string DiscountStrategy { get; set; }
        public string DiscountProductsOrCategories { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
