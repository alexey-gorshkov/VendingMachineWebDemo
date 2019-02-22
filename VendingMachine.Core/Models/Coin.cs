namespace VendingMachine.Core.Models
{
    // coin and price
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