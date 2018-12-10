namespace VendingMachineLibrary.Factories.Products
{
    public abstract class ProductBase : IProductBase
    {
        public string Name { get; set; }
        public int Price { get; }

        protected ProductBase(int price, string name)
        {
            Price = price;
            Name = name;
        }
    }
}