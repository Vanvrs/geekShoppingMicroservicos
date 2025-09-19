using AutoMapper;
using GeekShopping.ProductApi.Data.ValuerObjects;
using GeekShopping.ProductApi.Model;

namespace GeekShopping.ProductApi.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                // Configuração correta - mapeamento bidirecional
                cfg.CreateMap<ProductVO, Product>();
                cfg.CreateMap<Product, ProductVO>();

                // Ou você pode usar desta forma:
                // cfg.CreateMap<ProductVO, Product>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}