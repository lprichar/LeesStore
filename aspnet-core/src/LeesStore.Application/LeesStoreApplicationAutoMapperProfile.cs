using AutoMapper;
using LeesStore.Products;

namespace LeesStore
{
    public class LeesStoreApplicationAutoMapperProfile : Profile
    {
        public LeesStoreApplicationAutoMapperProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
        }
    }
}
