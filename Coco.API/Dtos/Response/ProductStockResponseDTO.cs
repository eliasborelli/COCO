namespace Coco.API.Dtos.Response
{
    public class ProductStockResponseDTO : BaseDTO
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public int? TotalStock { get; set; }
        public decimal? Amount { get; set; }
    }
}
