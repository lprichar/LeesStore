using AutoMapper;
using LeesStore.Products;

namespace LeesStore
{
    public class LeesStoreApplicationAutoMapperProfile : Profile
    {
        public LeesStoreApplicationAutoMapperProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
        }
    }
}
