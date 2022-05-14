namespace Coco.API.Dtos.Response
{
    public class ProductResponseDTO : BaseDTO
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal? Amount { get; set; }
        public virtual ICollection<CategoryResponseDTO> Categories { get; set; }
    }
}
