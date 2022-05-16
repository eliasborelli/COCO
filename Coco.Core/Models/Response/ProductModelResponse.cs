namespace Coco.Core.Models.Response
{
    public class ProductModelResponse
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public int? TotalStock { get; set; }
        public decimal? Amount { get; set; }
    }
}
