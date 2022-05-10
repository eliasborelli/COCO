namespace Coco.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
