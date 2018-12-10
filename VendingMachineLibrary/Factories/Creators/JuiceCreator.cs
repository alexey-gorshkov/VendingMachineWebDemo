using VendingMachineLibrary.Factories.Products;
using VendingMachineLibrary.Models;

namespace VendingMachineLibrary.Factories.Creators
{
    public class JuiceCreator : CreatorBase
    {
        public JuiceCreator(int availability, int price)
            : base(availability, price, "Juice", TypeProduct.Juice)
        {
        }

        public override ProductBase Create()
        {
            JuiceProduct juiceProduct = null;

            if (ValidateProduct())
            {
                juiceProduct = new JuiceProduct(Price, Name);
                Availability--;
            }

            return juiceProduct;
        }

        public override bool ValidateProduct()
        {
            // товар есть в наличии
            return Availability > 0;
        }
    }
}