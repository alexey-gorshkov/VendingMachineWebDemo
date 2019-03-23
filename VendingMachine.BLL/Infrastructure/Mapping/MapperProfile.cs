using AutoMapper;
using VendingMachine.BLL.DTO;
using VendingMachine.DAL.Entities;

namespace VendingMachine.BLL.Infrastructure.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<VMEntity, VendingMachineStateDTO>();

            CreateMap<Purse, PurseDTO>()
                .ForMember(x => x.Coins, map => map.MapFrom<CoinsResolver>());

            CreateMap<VMCreator, CreatorProductDTO>()
                .ForMember(x => x.Product, map => map.MapFrom( src => new ProductDTO { Name = src.Name, Price = src.Price } ));

            CreateMap<CustomerProduct, CustomerProductDTO>()
                .ForMember(x => x.Product, map => map.MapFrom( src => new ProductDTO { Name = src.Name, Price = src.Price } ) );
        }
    }
}