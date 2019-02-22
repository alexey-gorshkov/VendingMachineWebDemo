using VendingMachine.Core.Models;

namespace VendingMachine.BLL.DTO
{
    public class CoinDTO
    {
        public TypeCoin TypeCoin { get; set; }

        // цена монеты в int
        //public int Price => (int)this.TypeCoin;
    }
}