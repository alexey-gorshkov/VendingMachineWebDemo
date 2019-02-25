using VendingMachine.Core.Models;

namespace VendingMachine.BLL.Factories.Products
{
    public abstract class ProductBase : IProductBase
    {
        public string Name { get; set; }

        protected ProductBase(string name)
        {
            Name = name;
        }
    }
}