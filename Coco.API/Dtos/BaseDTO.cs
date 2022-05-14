using Newtonsoft.Json;

namespace Coco.API.Dtos
{
    [Serializable]
    public abstract class BaseDTO
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
