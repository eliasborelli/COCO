using Coco.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Core.Models.Response
{
    public class VirtualCartResponse
    {
        public string Code { get; set; }
        public string StoreName { get; set; }
        public IEnumerable<VirtualProductCartResponse> VirtualProductCarts { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
    }
}
