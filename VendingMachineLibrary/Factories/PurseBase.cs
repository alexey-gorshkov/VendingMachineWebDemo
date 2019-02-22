using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Core.Models;

namespace VendingMachine.BLL.Factories
{
    // Кошелек базовый класс
    public abstract class PurseBase
    {
        /// <summary>
        /// Монеты в кошельке
        /// </summary>
        public List<Coin> Coins { get; }

        protected PurseBase(List<Coin> coins)
        {
            Coins = coins;
        }

        /// <summary>
        /// Оплата по сумме
        /// </summary>
        /// <param name="summ"></param>
        /// <returns></returns>
        public abstract IEnumerable<Coin> Pay(int summ);

        /// <summary>
        /// Оплата определенными типами монет
        /// </summary>
        /// <param name="coins"></param>
        public abstract IEnumerable<Coin> Pay(IEnumerable<Coin> coins);

        /// <summary>
        /// Пополнить свой кошелек
        /// </summary>
        /// <param name="coins"></param>
        public abstract void Replenish(IEnumerable<Coin> coins);

        /// <summary>
        /// Проверка в кошельке доступности монет на сумму
        /// </summary>
        /// <param name="summ"></param>
        /// <returns></returns>
        public bool ValidateAmountCoins(int summ)
        {
            // получаем все подходящие монеты
            var coins = GetCoins(summ);
            // если мумма найденых монет совпадает с итоговой
            return summ == coins.Sum(x => x.Price); ;
        }

        /// <summary>
        /// Проверка нужных монет в кошельке по типу монет
        /// </summary>
        /// <param name="coins"></param>
        /// <returns></returns>
        public bool ValidateAmountCoins(IEnumerable<Coin> coins)
        {
            if (coins == null) throw new ArgumentNullException(nameof(coins));

            // получаем все подходящие монеты
            var coinsFind = GetCoins(coins);

            // проверяем все ли монеты нашли
            var coinsSum = coins.Sum(x => x.Price);
            var coinsFindSum = coinsFind.Sum(x => x.Price);
            return coinsSum == coinsFindSum;
        }

        /// <summary>
        /// Забираем монеты
        /// </summary>
        /// <param name="coins"></param>
        protected void RemoveCoins(IEnumerable<Coin> coins)
        {
            Coins.RemoveAll(coins.Contains);
        }

        /// <summary>
        /// Добавляем монеты
        /// </summary>
        /// <param name="coins"></param>
        protected void AddCoins(IEnumerable<Coin> coins)
        {
            Coins.AddRange(coins);
        }

        /// <summary>
        /// Получить монеты на сумму
        /// </summary>
        /// <param name="summ"></param>
        /// <returns></returns>
        protected IEnumerable<Coin> GetCoins(int summ)
        {
            List<Coin> resultList = new List<Coin>();
            // сортируем монеты по убыванию
            var orderCoin = Coins.OrderByDescending(x => x.TypeCoin);
            foreach (var coin in orderCoin)
            {
                if (summ > 0 && (summ - coin.Price) >= 0)
                {
                    summ -= coin.Price;
                    resultList.Add(coin);
                }
                else if (summ == 0)
                    break;
            }
            return resultList;
        }

        /// <summary>
        /// Получить монеты в кошельке определенного типа
        /// </summary>
        /// <param name="coins"></param>
        /// <returns></returns>
        protected IEnumerable<Coin> GetCoins(IEnumerable<Coin> coins)
        {
            var resultList = new List<Coin>();

            // перебираем монеты для поиска
            foreach (var coin in coins)
            {
                // находим все монеты такого номинала в кошельке
                var findCoins = Coins.Where(x => x.TypeCoin == coin.TypeCoin);
                foreach (var findCoin in findCoins)
                {
                    // ищем ту что еще не забрали монету
                    if (resultList.FirstOrDefault(x => x == findCoin) == null)
                    {
                        resultList.Add(findCoin);
                        break;
                    }
                }
            }

            return resultList;
        }
    }
}