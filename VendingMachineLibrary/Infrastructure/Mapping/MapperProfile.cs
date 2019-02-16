using AutoMapper;
using VendingMachine.BLL.DTO;
using VendingMachine.BLL.Factories;
using VendingMachine.BLL.Factories.Creators;
using VendingMachine.DAL.Entities;

namespace VendingMachine.BLL.Infrastructure.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //создаем маппинг
            CreateMap<VendingMachineState, VendingMachineStateDTO>();

            CreateMap<Purse, PurseDTO>()
                .ForMember(x => x.Coins, map => map.MapFrom<CoinsResolver>());


            CreateMap<CreatorBase, CreatorProductDTO>();
        }
    }
}