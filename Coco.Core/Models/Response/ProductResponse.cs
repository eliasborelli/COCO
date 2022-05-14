using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Core.Models.Response
{
    public class ProductResponse
    {
        public Guid ProductId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal? Amount { get; set; }
        public IEnumerable<CategoryResponse> Categories { get; set; }
    }
}
