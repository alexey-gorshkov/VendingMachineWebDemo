using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.BLL.DTO;
using VendingMachine.BLL.Services;
using VendingMachine.Core.Models;
using VendingMachine.DAL.Data;
using VendingMachine.DAL.Entities;
using VendingMachine.DAL.Repositories;
using VendingMachine.Tests.VM.BLL.Services.Fake;
using Xunit;

namespace VendingMachine.Tests.VM.BLL
{
    public class PaymentServiceTests
    {
        const string VMUSERNAME = "vending-machine@vmachine.com";
        const string CUSTOMER_USERNAME = "testuser@testuser.com";

        [Fact]
        public void AddAmountDepositAsyncTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase($"db-{Guid.NewGuid()}")
                .Options;

            // Run the test against one instance of the context
            using (var context = new ApplicationDbContext(options))
            {
                InitUsersAsync(context);

                var customerId = context.Users
                    .AsNoTracking().Where(x => x.UserName == CUSTOMER_USERNAME)
                    .Select(x => x.Id)
                    .SingleOrDefault();

                var purseRepository = new PurseRepository(context);
                var userDepositRepository = new UserDepositRepository(context);
                var customerProductRepository = new CustomerProductRepository(context);
                var vendingMachineService = new VendingMachineServiceFake(purseRepository, context);

                var paymentService = new PaymentService(purseRepository, userDepositRepository,
                    customerProductRepository, vendingMachineService);

                paymentService.AddAmountDepositAsync(new CoinDTO { TypeCoin = TypeCoin.Price10Rub }, customerId).GetAwaiter();

                // у юзера депозит равен 10
                Assert.Equal(10, userDepositRepository.GetAmountDepositAsync(customerId).GetAwaiter().GetResult());

                // кошелек VM
                var purseVM = context.Users.AsNoTracking().Where(x=>x.UserName == VMUSERNAME)
                    .Include(x => x.Purse)
                    .ThenInclude(x => x.PurseCoins)
                    .SingleOrDefault();

                var purseCoins10 = purseVM.Purse.PurseCoins
                    .Where(x => x.TypeCoin == TypeCoin.Price10Rub)
                    .SingleOrDefault();

                // у автомата в кошельке монет достоинством 10р будет 101 штука
                Assert.Equal(101, purseCoins10.Count);
            }
        }

        private void InitUsersAsync(ApplicationDbContext context)
        {
            // new users and purses
            var users = new User[2]
            {
                new User {
                    UserName = VMUSERNAME,
                    Email = VMUSERNAME,
                    Purse = new Purse
                    {
                        PurseCoins = new List<PurseCoin>
                        {
                            new PurseCoin { Id = Guid.NewGuid(), TypeCoin = TypeCoin.Price1Rub, Count = 100 },
                            new PurseCoin { Id = Guid.NewGuid(), TypeCoin = TypeCoin.Price2Rub, Count = 100 },
                            new PurseCoin { Id = Guid.NewGuid(), TypeCoin = TypeCoin.Price5Rub, Count = 100 },
                            new PurseCoin { Id = Guid.NewGuid(), TypeCoin = TypeCoin.Price10Rub, Count = 100 }
                        }
                    }
                },
                new User {
                    UserName = CUSTOMER_USERNAME,
                    Email = "testuser@testuser.com",
                    Purse = new Purse
                    {
                        PurseCoins = new List<PurseCoin>
                        {
                            new PurseCoin { Id = Guid.NewGuid(), TypeCoin = TypeCoin.Price1Rub, Count = 10 },
                            new PurseCoin { Id = Guid.NewGuid(), TypeCoin = TypeCoin.Price2Rub, Count = 30 },
                            new PurseCoin { Id = Guid.NewGuid(), TypeCoin = TypeCoin.Price5Rub, Count = 20 },
                            new PurseCoin { Id = Guid.NewGuid(), TypeCoin = TypeCoin.Price10Rub, Count = 15 }
                        }
                    }
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
