using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.BLL.DTO;
using VendingMachine.BLL.Factories;

namespace VendingMachineWebAPI.Infrastructure.Mappings
{
    public class CoinsResolver : IValueResolver<PurseBase, PurseDTO, IEnumerable<CoinCountDTO>>
    {
        public IEnumerable<CoinCountDTO> Resolve(PurseBase source, PurseDTO destination, IEnumerable<CoinCountDTO> member,
            ResolutionContext context)
        {
            IEnumerable<CoinCountDTO> grCoins = source.Coins
                .GroupBy(x => x.TypeCoin)
                .Select(gr => new CoinCountDTO {
                    Count = gr.Count(),
                    TypeCoin = gr.Key
                })
                .OrderBy(x=>x.TypeCoin)
                .ToList();

            return grCoins;
        }
    }
}