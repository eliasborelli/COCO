using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Core.Models.Request
{
    public class VirtualCartProductsRemoveFilter
    {
        public string VirtualCartCode { get; set; }
        public List<string> Products { get; set; }
    }
}
