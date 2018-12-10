
namespace VendingMachineLibrary.Factories.Products
{
    interface IProductBase
    {
        string Name { get; set; }
        int Price { get; }
    }
}
