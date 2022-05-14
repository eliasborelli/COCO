using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Core.Models.Response
{
    public class StoreResponse
    {
        public Guid StoreId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public IEnumerable<StockResponse> Stocks { get; set; }
        public IEnumerable<VoucherResponse> Vouchers { get; set; }
        public WorkingDaysResponse WorkingDay { get; set; }
    }
}
