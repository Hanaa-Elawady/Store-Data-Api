using AutoMapper;
using Store.Data.Entities;
using Store.Service.Services.ProductServices;

namespace Store.Service.Dtos.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.BrandName, options => options.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.TypeName, options => options.MapFrom(src => src.Type.Name))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom<ProductPictureUrlResolver>());

            CreateMap<ProductBrand, BrandsDto>();
            CreateMap<ProductType, TypeDto>();
        }
    }
}
