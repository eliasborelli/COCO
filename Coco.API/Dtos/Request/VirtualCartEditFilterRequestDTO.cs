namespace Coco.API.Dtos.Request
{
    public class VirtualCartEditFilterRequestDTO : BaseDTO
    {
        public string VirtualCartCode { get; set; }
        public List<VirtualProductFilterRequestDTO> Products { get; set; }
    }
}
