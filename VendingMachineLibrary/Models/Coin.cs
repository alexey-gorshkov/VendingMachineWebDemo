using VendingMachine.Core.Models;

namespace VendingMachine.BLL.Models
{
    // монета и количество
    public class Coin
    {
        public TypeCoin TypeCoin { get; set; }

        public Coin(TypeCoin typeCoin)
        {
            TypeCoin = typeCoin;
        }

        // цена монеты в int
        public int Price => (int)this.TypeCoin;
    }
}