﻿using VendingMachine.BLL.Factories.Products;
using VendingMachine.Core.Models;

namespace VendingMachine.BLL.Factories.Creators
{
    public class CoffeeWithMilkCreator : CreatorBase
    {
        public CoffeeWithMilkCreator(int availability, int price)
            : base(availability, price, "Coffee With Milk", TypeProduct.CoffeeWithMilk)
        {
        }

        public override ProductBase Create()
        {
            CoffeeWithMilkProduct cofeeWithMilkProduct = null;

            if (ValidateProduct())
            {
                cofeeWithMilkProduct = new CoffeeWithMilkProduct(Price, Name);
                Availability--;
            }

            return cofeeWithMilkProduct;
        }

        public override bool ValidateProduct()
        {
            // товар есть в наличии
            return Availability > 0;
        }
    }
}