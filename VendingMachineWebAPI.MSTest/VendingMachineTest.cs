using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineLibrary;
using VendingMachineLibrary.Factories;
using VendingMachineLibrary.Factories.Creators;
using VendingMachineLibrary.Models;

namespace VendingMachineWebAPI.MSTest
{
    [TestClass]
    public class VendingMachineTest
    {
        private VendingMachine vendingMachine;

        public VendingMachineTest()
        {
            vendingMachine = new VendingMachine();
        }

        [TestMethod]
        // после инициализации в кошельке ћашины есть по 100 монет каждого наминала
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
        // после инициализации в кошельке ѕокупател€ есть заданное количество монет по “«
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
        // после инициализации машины должны быть соответсвующие —оздатели
        public void TestCreatorsInit()
        {
            var creators = vendingMachine.Creators;

            Assert.IsInstanceOfType(creators.First(x=>x.TypeProduct == TypeProduct.Tea), typeof(TeaCreator));
            Assert.IsInstanceOfType(creators.First(x=>x.TypeProduct == TypeProduct.Coffee), typeof(CoffeeCreator));
            Assert.IsInstanceOfType(creators.First(x=>x.TypeProduct == TypeProduct.CoffeeWithMilk), typeof(CoffeeWithMilkCreator));
            Assert.IsInstanceOfType(creators.First(x=>x.TypeProduct == TypeProduct.Juice), typeof(JuiceCreator));
        }

        [TestMethod]
        // юзер вносит монету 10р провер€ем что монета попала в кошелек VM
        public void PushAmmountDepositedUser10R()
        {
            var startCountMoney10R = vendingMachine.PurseVM
                .Coins.Count(x => x.TypeCoin == TypeCoin.Price10Rub);

            var coins = new List<Coin>
            {
                new Coin(TypeCoin.Price10Rub)
            };

            vendingMachine.PushAmmountDeposited(coins);

            Assert.AreEqual(startCountMoney10R+1, vendingMachine.PurseVM.Coins.Count(x => x.TypeCoin == TypeCoin.Price10Rub));
        }

        [TestMethod]
        // возвращаем сдачу покупателю из депозита VM
        public void GetSurrenderUser()
        {
            // кидаем монету
            var coins = new List<Coin>
            {
                new Coin(TypeCoin.Price10Rub)
            };

            // сумма денег у юзера
            var startSumMoneyUser = vendingMachine.PurseUser.Coins.Sum(x => x.Price);

            // вносим монету
            vendingMachine.PushAmmountDeposited(coins);

            // возвращаем депозит юзеру
            vendingMachine.GetSurrenderUser();

            Assert.AreEqual(startSumMoneyUser, vendingMachine.PurseUser.Coins.Sum(x => x.Price));
        }
    }
}
