using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Core.Entities
{
    public class VirtualProductCart : BaseEntity
    {
        public Guid VirtualCartId { get; set; }
        public virtual Product Product { get; set; }
        public int? Quantity { get; set; }
        public decimal? DiscountAmount { get; set; }
    }
}
