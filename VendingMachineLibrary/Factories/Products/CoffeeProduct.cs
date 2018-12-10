using VendingMachineLibrary.Factories.Products;

namespace VendingMachineLibrary.Factories
{
    public class CoffeeProduct : ProductBase
    {
        public CoffeeProduct(int price, string name) : base(price, name)
        {
        }
    }
}