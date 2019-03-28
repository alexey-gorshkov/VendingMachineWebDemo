using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using VendingMachine.Core.Models;
using VendingMachine.DAL.Data;
using VendingMachine.DAL.Entities;
using VendingMachine.DAL.Repositories;
using Xunit;

namespace VendingMachine.Tests.VM.DAL
{
    public class UserDepositRepositoryTests
    {
        [Fact]
        public void AddAmountDepositAsyncTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase($"db-{Guid.NewGuid()}")
                .Options;

            // Run the test against one instance of the context
            using (var context = new ApplicationDbContext(options))
            {
                var userId = InitUser(context);

                var userDepositRepository = new UserDepositRepository(context);
                userDepositRepository.AddAmountDepositAsync((int)TypeCoin.Price10Rub, userId).GetAwaiter().GetResult();

                Assert.Equal(10, userDepositRepository.GetAmountDepositAsync(userId).GetAwaiter().GetResult());
            }
        }

        [Fact]
        public void RetrieveDepositAsyncTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase($"db-{Guid.NewGuid()}")
                .Options;

            // Run the test against one instance of the context
            using (var context = new ApplicationDbContext(options))
            {
                var userId = InitUser(context);

                var userDepositRepository = new UserDepositRepository(context);
                userDepositRepository.AddAmountDepositAsync((int)TypeCoin.Price1Rub, userId).GetAwaiter().GetResult();
                userDepositRepository.RetrieveDepositAsync(userId).GetAwaiter().GetResult();
                Assert.Equal(0, userDepositRepository.GetAmountDepositAsync(userId).GetAwaiter().GetResult());
            }
        }

        private Guid InitUser(ApplicationDbContext context)
        {
            // new user and purse
            var user = new User
            {
                UserName = "testuser@testuser.com",
                Email = "testuser@testuser.com",
                Purse = new Purse
                {
                    PurseCoins = new List<PurseCoin>
                    {
                        new PurseCoin { TypeCoin = TypeCoin.Price1Rub, Count = 10 },
                        new PurseCoin { TypeCoin = TypeCoin.Price2Rub, Count = 30 },
                        new PurseCoin { TypeCoin = TypeCoin.Price5Rub, Count = 20 },
                        new PurseCoin { TypeCoin = TypeCoin.Price10Rub, Count = 15 }
                    }
                }
            };

            context.Users.Add(user);
            context.SaveChanges();
            return user.Id;
        }
    }
}
