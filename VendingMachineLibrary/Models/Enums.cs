using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineLibrary.Models
{
    // тип монеты
    public enum TypeCoin : int
    {
        Price1Rub = 1,
        Price2Rub = 2,
        Price5Rub = 5,
        Price10Rub = 10,
    }

    public enum TypeProduct
    {
        Coffee = 1,
        CoffeeWithMilk = 2,
        Tea = 3,
        Juice = 4
    }
}
