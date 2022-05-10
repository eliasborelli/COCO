using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Core.Entities
{
    public class Product
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal? Amount { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
