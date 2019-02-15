using VendingMachine.BLL.Factories.Products;

namespace VendingMachine.BLL.Factories
{
    public class CoffeeProduct : ProductBase
    {
        public CoffeeProduct(int price, string name) : base(price, name)
        {
        }
    }
}