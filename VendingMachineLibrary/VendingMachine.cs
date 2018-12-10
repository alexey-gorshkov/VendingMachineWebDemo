using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachineLibrary.Factories;
using VendingMachineLibrary.Factories.Creators;
using VendingMachineLibrary.Factories.Products;
using VendingMachineLibrary.Models;

namespace VendingMachineLibrary
{
    public class VendingMachine
    {
        // депозит юзера, сумма что он внес в монетоприемник
        private int _amountDeposited { get; set; }

        // set samples data
        public VendingMachine()
        {
            InitPurseVM();
            InitPurseUser();
            InitCreators();
        }

        // custom params machine and user
        public VendingMachine(IEnumerable<CreatorBase> creators,
            List<Coin> coinsVM,
            List<Coin> coinsUser)
        {
            PurseUser = new PurseUser(coinsUser);
            PurseVM = new PurseVM(coinsVM);
            Creators = creators;
        }

        // available creators
        public IEnumerable<CreatorBase> Creators { get; set; }

        // purse VM
        public PurseBase PurseVM { get; set; }

        // purse user
        public PurseBase PurseUser { get; set; }

        /// <summary>
        /// Депозит юзера
        /// </summary>
        public int AmountDeposited => _amountDeposited;

        /// <summary>
        /// Пополняем депозит монетами юзера
        /// </summary>
        /// <param name="coins"></param>
        /// <returns></returns>
        public int PushAmmountDeposited(IEnumerable<Coin> coins)
        {
            if (coins == null) throw new ArgumentNullException(nameof(coins));
            if (PurseUser.ValidateAmountCoins(coins))
            {
                var coinsUser = PurseUser.Pay(coins);
                PurseVM.Replenish(coinsUser);
                var summCoins = coinsUser.Sum(x => x.Price);
                _amountDeposited += summCoins;
            }

            return AmountDeposited;
        }

        // выдача товара из машины
        public ProductBase CreateProduct(TypeProduct typeProduct)
        {
            ProductBase product = null;

            // пользователь не внес депозит
            if (AmountDeposited == 0)
                return null;

            CreatorBase creator = Creators
                .FirstOrDefault(x => x.TypeProduct == typeProduct);

            // хватает денег у юзера
            if (creator != null && creator.Price <= AmountDeposited)
            {
                product = creator.Create();
                if (product != null)
                    ProcessPay(creator.Price);
            }

            return product;
        }

        /// <summary>
        /// Процесс покупки
        /// </summary>
        /// <param name="price"></param>
        private void ProcessPay(int price)
        {
            _amountDeposited -= price;
        }

        /// <summary>
        /// Вернуть депозит покупателю c кошелька VM
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Coin> GetSurrenderUser()
        {
            // имеется депозит у юзера
            if (AmountDeposited > 0)
            {
                IEnumerable<Coin> coinsVM = null;
                // проверяем имеются ли монеты для сдачи в кошельке ВМ
                if (PurseVM.ValidateAmountCoins(AmountDeposited))
                {
                    coinsVM = PurseVM.Pay(AmountDeposited);
                    PurseUser.Replenish(coinsVM);
                    _amountDeposited = 0;
                }

                return coinsVM;
            }
            return null;
        }

        #region Init data
        /// <summary>
        /// Init purse VM money
        /// </summary>
        private void InitPurseVM()
        {
            List<Coin> coinsVM = new List<Coin>();
            for (int i = 0; i < 100; i++)
                coinsVM.AddRange(new List<Coin> {
                    new Coin(TypeCoin.Price1Rub),
                    new Coin(TypeCoin.Price2Rub),
                    new Coin(TypeCoin.Price5Rub),
                    new Coin(TypeCoin.Price10Rub)
                });

            PurseVM = new PurseVM(coinsVM);
        }

        // инициализируем монеты пользователя
        private void InitPurseUser()
        {
            List<Coin> coinsUser = new List<Coin>();
            for (int i = 0; i < 10; i++)
                coinsUser.Add(new Coin(TypeCoin.Price1Rub));
            for (int i = 0; i < 30; i++)
                coinsUser.Add(new Coin(TypeCoin.Price2Rub));
            for (int i = 0; i < 20; i++)
                coinsUser.Add(new Coin(TypeCoin.Price5Rub));
            for (int i = 0; i < 15; i++)
                coinsUser.Add(new Coin(TypeCoin.Price10Rub));

            PurseUser = new PurseUser(coinsUser);
        }

        // инициализируем базовый набор товаров
        private void InitCreators()
        {
            Creators = new List<CreatorBase>
            {
                new TeaCreator(10, 13),
                new CoffeeCreator(20, 18),
                new CoffeeWithMilkCreator(20, 21),
                new JuiceCreator(15, 35)
            };
        }
        #endregion
    }
}
