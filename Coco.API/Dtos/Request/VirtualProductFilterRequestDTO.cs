namespace Coco.API.Dtos.Request
{
    public class VirtualProductFilterRequestDTO : BaseDTO
    {
        public string Code { get; set; }
        public int Quantity { get; set; }
    }
}
