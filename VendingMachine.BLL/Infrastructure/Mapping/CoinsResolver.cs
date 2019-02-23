using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.BLL.DTO;
using VendingMachine.DAL.Entities;

namespace VendingMachine.BLL.Infrastructure.Mappings
{
    public class CoinsResolver : IValueResolver<Purse, PurseDTO, IEnumerable<CoinCountDTO>>
    {
        public IEnumerable<CoinCountDTO> Resolve(Purse source, PurseDTO destination, IEnumerable<CoinCountDTO> member,
            ResolutionContext context)
        {
            IEnumerable<CoinCountDTO> grCoins = source.PurseCoins
                .GroupBy(x => x.TypeCoin)
                .Select(gr => new CoinCountDTO {
                    Count = gr.Sum(g => g.Count),
                    TypeCoin = gr.Key
                })
                .OrderBy(x=>x.TypeCoin)
                .ToList();

            return grCoins;
        }
    }
}