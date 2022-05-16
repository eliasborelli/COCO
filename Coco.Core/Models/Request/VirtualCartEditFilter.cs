namespace Coco.Core.Models.Request
{
    public class VirtualCartEditFilter
    {
        public string VirtualCartCode { get; set; }
        public List<VirtualProductFilter> Products { get; set; }
    }
}
