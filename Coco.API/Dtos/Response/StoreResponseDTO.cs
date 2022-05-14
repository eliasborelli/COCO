namespace Coco.API.Dtos.Response
{
    public class StoreResponseDTO : BaseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
