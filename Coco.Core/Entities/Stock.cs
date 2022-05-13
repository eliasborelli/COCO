namespace Coco.Core.Entities
{
    public class Stock : BaseEntity
    {
        public Guid StoreId { get; set; }
        public Guid ProductId { get; set; }
        public int? CurrentStock { get; set; }
        public virtual Product Product { get; set; }
    }
}
