using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Core.Models.Response
{
    public class VirtualProductCartResponse
    {
        public ProductResponse Product { get; set; }
        public int? Quantity { get; set; }
        public decimal? DiscountAmount { get; set; }
    }
}
