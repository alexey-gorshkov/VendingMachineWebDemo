using VendingMachine.BLL.Factories.Products;
using VendingMachine.Core.Models;

namespace VendingMachine.BLL.Factories.Creators
{
    public abstract class CreatorBase
    {
        /// <summary>
        /// Доступные порции
        /// </summary>
        public int Availability { get; set; }
        public int Price { get; set; }
        public string Name { get; }
        public TypeProduct TypeProduct { get; }

        protected CreatorBase(int availability, int price, string name, TypeProduct typeProduct)
        {
            Availability = availability;
            Price = price;
            Name = name;
            TypeProduct = typeProduct;
        }

        public abstract ProductBase Create();

        public abstract bool ValidateProduct();
    }
}