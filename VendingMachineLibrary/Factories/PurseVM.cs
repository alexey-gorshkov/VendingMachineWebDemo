using System.Collections.Generic;
using VendingMachineLibrary.Models;

namespace VendingMachineLibrary.Factories
{
    // кошелек Vending Machine
    public class PurseVM : PurseBase
    {
        public PurseVM(List<Coin> coins) : base(coins)
        {
        }

        // заплатить по сумме подходящими монетами
        public override IEnumerable<Coin> Pay(int summ)
        {
            IEnumerable<Coin> resultList = null;
            if (ValidateAmountCoins(summ))
            {
                resultList = GetCoins(summ);
                // забираем монетки
                RemoveCoins(resultList);
            }
            return resultList;
        }

        // заплатить определенным типом монет
        public override IEnumerable<Coin> Pay(IEnumerable<Coin> coins)
        {
            IEnumerable<Coin> resultList = null;
            if (ValidateAmountCoins(coins))
            {
                resultList = GetCoins(coins);
                // забираем монетки
                RemoveCoins(resultList);
            }
            return resultList;
        }

        // пополнить свой кошелек
        public override void Replenish(IEnumerable<Coin> coins)
        {
            AddCoins(coins);
        }
    }
}