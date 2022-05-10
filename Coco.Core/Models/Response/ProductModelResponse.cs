using Coco.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coco.Core.Models.Response
{
    public class ProductModelResponse
    {
        public Product Product { get; set; }
        public int? CurrentStock { get; set; }
    }
}
