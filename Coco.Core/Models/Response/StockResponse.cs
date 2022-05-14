using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Core.Models.Response
{
    public class StockResponse
    {
        public Guid StockId { get; set; }
        public int? CurrentStock { get; set; }
        public ProductResponse Product { get; set; }
    }
}
