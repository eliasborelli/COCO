namespace Coco.Core.Models.Request
{
    public class VirtualCartFilter
    {
        public string Store { get; set; }
        public List<VirtualProductFilter> Products { get; set; }
    }

    public class VirtualProductFilter
    {
        public string Code { get; set; }
        public int Quantity { get; set; }
    }
}
