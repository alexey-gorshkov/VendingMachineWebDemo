using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachine.BLL.DTO;
using VendingMachine.BLL.Factories;
using VendingMachine.BLL.Factories.Creators;
using VendingMachine.BLL.Factories.Products;
using VendingMachine.BLL.Interfaces;
using VendingMachine.BLL.Models;
using VendingMachine.Core.Models;
using VendingMachine.DAL.Entities;
using VendingMachine.DAL.Interfaces;

namespace VendingMachine.BLL
{
    public class VendingMachineService : IVendingMachineService
    {
        const string VMUSERNAME = "vending-machine@vmachine.com";

        private readonly IPurseRepository _purseRepository;
        private readonly IUserDepositRepository _userDepositRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public VendingMachineService(IPurseRepository purseRepository,
            IUserDepositRepository userDepositRepository,
            IMapper mapper,
            UserManager<User> userManager)
        {
            _purseRepository = purseRepository;
            _userDepositRepository = userDepositRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        // депозит юзера, сумма что он внес в монетоприемник
        private int _amountDeposited { get; set; }

        // set samples data
        //public VendingMachineService()
        //{
        //    InitPurseVM();
        //    InitPurseUser();
        //    InitCreators();
        //}

        // custom params machine and user
        //public VendingMachineService(IEnumerable<CreatorBase> creators,
        //    List<Coin> coinsVM,
        //    List<Coin> coinsUser)
        //{
        //    PurseUser = new PurseUser(coinsUser);
        //    PurseVM = new PurseVM(coinsVM);
        //    Creators = creators;
        //}

        public async Task<VendingMachineStateDTO> GetUserStateAsync(User customer)
        {
            var deposit = await _userDepositRepository.GetAll()
                .Where(x => x.UserId == customer.Id)
                .SingleOrDefaultAsync();

            var creators = GetCreators();
            var creatorsDTO = _mapper.Map<List<CreatorBase>, List<CreatorProductDTO>>(creators);
            var purseCustomer = await _purseRepository.GetPurseAndCoinsAsync(customer.Id);
            var vmUser = await _userManager.FindByNameAsync(VMUSERNAME);
            var purseVM = await _purseRepository.GetPurseAndCoinsAsync(vmUser.Id);

            return new VendingMachineStateDTO
            {
                AmountDeposited = deposit?.AmountOfDeposit ?? 0,
                Creators = creatorsDTO,
                PurseCustomer = _mapper.Map<Purse, PurseDTO>(purseCustomer),
                PurseVM = _mapper.Map<Purse, PurseDTO>(purseVM)
            };
        }

        // available creators
      //  public IEnumerable<CreatorBase> Creators { get; set; }

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

            CreatorBase creator = GetCreators()
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

        // init creators
        private List<CreatorBase> GetCreators()
        {
            var creators = new List<CreatorBase>
            {
                new TeaCreator(10, 13),
                new CoffeeCreator(20, 18),
                new CoffeeWithMilkCreator(20, 21),
                new JuiceCreator(15, 35)
            };
            return creators;
        }
        #endregion
    }
}
