using VendingMachine.Core.Models;

namespace VendingMachine.BLL.DTO
{
    public class CoinCountDTO
    {
        public TypeCoin TypeCoin { get;set;}
        public int Count { get; set; }
    }
}