using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Core.Models.Request
{
    public class VirtualCartFilter
    {
        public string NameStore { get; set; }
        public List<VirtualProductFilter> Products { get; set; }
    }

    public class VirtualProductFilter
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}
