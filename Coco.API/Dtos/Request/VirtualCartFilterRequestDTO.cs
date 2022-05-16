namespace Coco.API.Dtos.Request
{
    public class VirtualCartFilterRequestDTO : BaseDTO
    {
        public string Store { get; set; }
        public List<VirtualProductFilterRequestDTO> Products { get; set; }
    }
}
