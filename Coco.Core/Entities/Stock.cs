namespace Coco.Core.Entities
{
    public class Stock
    {
        public virtual Product Product { get; set; }
        public int? CurrentStock { get; set; }
    }
}
