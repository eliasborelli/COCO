namespace Coco.Core.Entities
{
    public class VirtualCart : BaseEntity
    {
        public string Code { get; set; }
        public Guid StoreId { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<VirtualProductCart> VirtualProductCarts { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
    }
}
