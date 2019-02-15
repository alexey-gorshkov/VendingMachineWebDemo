using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine.BLL;
using VendingMachine.BLL.Factories;
using VendingMachine.BLL.Factories.Creators;
using VendingMachine.BLL.Models;
using VendingMachine.Core.Models;

namespace VendingMachineWebAPI.MSTest
{
    [TestClass]
    public class VendingMachineTest
    {
        private VendingMachineService vendingMachine;

        public VendingMachineTest()
        {
            vendingMachine = new VendingMachineService();
        }

        [TestMethod]
        // после инициализации в кошельке Машины есть по 100 монет каждого наминала
        public void TestPuseVMInit()
        {
            PurseBase purse = vendingMachine.PurseVM;

            Assert.AreEqual(100, purse.Coins
                .Count(x => x.TypeCoin == TypeCoin.Price10Rub));

            Assert.AreEqual(100, purse.Coins
                .Count(x => x.TypeCoin == TypeCoin.Price5Rub));

            Assert.AreEqual(100, purse.Coins
                .Count(x => x.TypeCoin == TypeCoin.Price2Rub));

            Assert.AreEqual(100, purse.Coins
                .Count(x => x.TypeCoin == TypeCoin.Price1Rub));
        }

        [TestMethod]
        // после инициализации в кошельке Покупателя есть заданное количество монет
        public void TestPuseUserInit()
        {
            PurseBase purse = vendingMachine.PurseUser;

            Assert.AreEqual(15, purse.Coins
                .Count(x => x.TypeCoin == TypeCoin.Price10Rub));

            Assert.AreEqual(20, purse.Coins
                .Count(x => x.TypeCoin == TypeCoin.Price5Rub));

            Assert.AreEqual(30, purse.Coins
                .Count(x => x.TypeCoin == TypeCoin.Price2Rub));

            Assert.AreEqual(10, purse.Coins
                .Count(x => x.TypeCoin == TypeCoin.Price1Rub));
        }

        [TestMethod]
        // после инициализации машины должны быть соответсвующие Создатели
        public void TestCreatorsInit()
        {
            var creators = vendingMachine.Creators;

            Assert.IsInstanceOfType(creators.First(x=>x.TypeProduct == TypeProduct.Tea), typeof(TeaCreator));
            Assert.IsInstanceOfType(creators.First(x=>x.TypeProduct == TypeProduct.Coffee), typeof(CoffeeCreator));
            Assert.IsInstanceOfType(creators.First(x=>x.TypeProduct == TypeProduct.CoffeeWithMilk), typeof(CoffeeWithMilkCreator));
            Assert.IsInstanceOfType(creators.First(x=>x.TypeProduct == TypeProduct.Juice), typeof(JuiceCreator));
        }

        [TestMethod]
        // юзер вносит монету 10р проверяем что монета попала в кошелек VM
        public void PushAmmountDepositedUser10R()
        {
            // количество монет у машины
            var startCountMoney10R = vendingMachine.PurseVM
                .Coins.Count(x => x.TypeCoin == TypeCoin.Price10Rub);

            var coins = new List<Coin>
            {
                new Coin(TypeCoin.Price10Rub)
            };
            // кидаем монетку 10р
            vendingMachine.PushAmmountDeposited(coins);

            // проверяем количество монет достоинства 10р в кошельке машины
            Assert.AreEqual(startCountMoney10R + 1, vendingMachine.PurseVM
                .Coins.Count(x => x.TypeCoin == TypeCoin.Price10Rub));
        }

        [TestMethod]
        // возвращаем сдачу покупателю из депозита VM
        public void GetSurrenderUser()
        {
            // 1 монета достоинства 10р
            var coins = new List<Coin>
            {
                new Coin(TypeCoin.Price10Rub)
            };

            // сумма денег у юзера
            var startSumMoneyUser = vendingMachine.PurseUser
                .Coins.Sum(x => x.Price);

            // сумма денег в машине
            var startSumMoneyVM = vendingMachine.PurseVM
                .Coins.Sum(x => x.Price);

            // вносим монету в машину
            vendingMachine.PushAmmountDeposited(coins);

            // проверяем уменьшилась ли сумма у юзера
            Assert.AreEqual(startSumMoneyUser - (int)TypeCoin.Price10Rub, vendingMachine.PurseUser
                .Coins.Sum(x => x.Price));

            // проверяем увеличилась ли сумма у машины
            Assert.AreEqual(startSumMoneyVM + (int)TypeCoin.Price10Rub, vendingMachine.PurseVM
                .Coins.Sum(x => x.Price));

            // возвращаем депозит юзеру
            vendingMachine.GetSurrenderUser();

            // сверяем сумма у юзера должна остатсья таже что и в начале теста
            Assert.AreEqual(startSumMoneyUser, vendingMachine.PurseUser
                .Coins.Sum(x => x.Price));

            // сверяем сумма у машины должна остатсья таже что и в начале теста
            Assert.AreEqual(startSumMoneyVM, vendingMachine.PurseVM
                .Coins.Sum(x => x.Price));
        }
    }
}
