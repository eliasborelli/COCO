namespace Coco.API.Dtos.Request
{
    public class VoucherFilterRequestDTO : BaseDTO
    {
        public string VoucherCode { get; set; }
        public string VirtualCartCode { get; set; }
    }
}
