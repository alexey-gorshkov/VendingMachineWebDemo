using VendingMachineLibrary.Factories.Products;
using VendingMachineLibrary.Models;

namespace VendingMachineLibrary.Factories.Creators
{
    public class CoffeeCreator : CreatorBase
    {
        public CoffeeCreator(int availability, int price)
            : base(availability, price, "Coffee", TypeProduct.Coffee)
        {

        }

        public override ProductBase Create()
        {
            CoffeeProduct coffeeProduct = null;

            if (ValidateProduct())
            {
                coffeeProduct = new CoffeeProduct(Price, Name);
                Availability--;
            }

            return coffeeProduct;
        }

        public override bool ValidateProduct()
        {
            // товар есть в наличии
            return Availability > 0;
        }
    }
}