namespace Coco.API.Dtos.Request
{
    public class VirtualCartRemoveFilterRequestDTO : BaseDTO
    {
        public string VirtualCartCode { get; set; }
        public List<string> Products { get; set; }
    }
}
