using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Core.Models.Response
{
    public class CategoryResponse
    {
        public Guid CategoryId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
