using AutoMapper;
using Coco.API.Dtos.Request;
using Coco.API.Dtos.Response;
using Coco.Core.Entities;
using Coco.Core.Models.Request;
using Coco.Core.Models.Response;

namespace Coco.API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductModelResponse, ProductStockResponseDTO>();







            CreateMap<ProductFilterRequestDTO, ProductFilter>();

            CreateMap<Category, CategoryResponseDTO>();
            CreateMap<Product, ProductResponseDTO>();

            CreateMap<Store, StoreResponseDTO>();
        }
    }
}
