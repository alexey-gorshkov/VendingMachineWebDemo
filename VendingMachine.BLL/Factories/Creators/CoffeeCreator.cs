using VendingMachine.BLL.Factories.Products;
using VendingMachine.Core.Models;

namespace VendingMachine.BLL.Factories.Creators
{
    public class CoffeeCreator : CreatorBase
    {
        public CoffeeCreator(int availability)
            : base(availability, "Coffee", TypeProduct.Coffee)
        {

        }

        public override ProductBase Create()
        {
            CoffeeProduct coffeeProduct = null;

            if (ValidateProduct())
            {
                coffeeProduct = new CoffeeProduct(Name);
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