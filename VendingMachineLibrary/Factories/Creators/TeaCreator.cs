using VendingMachine.BLL.Factories.Products;
using VendingMachine.Core.Models;

namespace VendingMachine.BLL.Factories.Creators
{
    public class TeaCreator : CreatorBase
    {
        public TeaCreator(int availability, int price)
            : base(availability, price, "Tea", TypeProduct.Tea)
        {
        }

        public override ProductBase Create()
        {
            TeaProduct teaProduct = null;

            if (ValidateProduct())
            {
                teaProduct = new TeaProduct(Price, Name);
                Availability--;
            }

            return teaProduct;
        }

        public override bool ValidateProduct()
        {
            // товар есть в наличии
            return Availability > 0;
        }
    }
}