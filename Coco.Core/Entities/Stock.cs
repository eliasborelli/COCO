namespace Coco.Core.Entities
{
    public class Stock : BaseEntity
    {
        public virtual Product Product { get; set; }
        public int? CurrentStock { get; set; }
    }
}
