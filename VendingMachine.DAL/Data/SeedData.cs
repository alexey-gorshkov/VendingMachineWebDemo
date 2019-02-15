using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using VendingMachine.Core.Models;
using VendingMachine.DAL.Entities;

namespace VendingMachine.DAL.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var context = provider.GetRequiredService<ApplicationDbContext>();
                var userManager = provider.GetRequiredService<UserManager<User>>();

                context.Database.Migrate();
                InitUsers(userManager);
            }
        }

        private static void InitUsers(UserManager<User> userManager)
        {
            MyUser[] users = new MyUser[2]
            {
                new MyUser
                {
                    ApplicationUser = new User
                    {
                        UserName = "vending-machine@vmachine.com",
                        Email = "vending-machine@vmachine.com",
                        Purse = new Purse
                        {
                            PurseCoins = new List<PurseCoin>
                            {
                                new PurseCoin { TypeCoin = TypeCoin.Price1Rub, Count = 100 },
                                new PurseCoin { TypeCoin = TypeCoin.Price2Rub, Count = 100 },
                                new PurseCoin { TypeCoin = TypeCoin.Price5Rub, Count = 100 },
                                new PurseCoin { TypeCoin = TypeCoin.Price10Rub, Count = 100 }
                            }
                        }
                    },
                    Password = "BDC1F1F6BC2949B1B3DE522B912EE589"
                },
                new MyUser
                {
                    ApplicationUser = new User
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
                    },
                    Password = "testuser"
                }
            };

            foreach (var user in users)
            {
                var findUser = userManager.FindByNameAsync(user.ApplicationUser.UserName).Result;
                if (findUser == null)
                {
                    var createPowerUser = userManager.CreateAsync(user.ApplicationUser, user.Password).Result;
                    if (createPowerUser.Succeeded)
                    {
                        var confirmationToken = userManager.GenerateEmailConfirmationTokenAsync(user.ApplicationUser).Result;
                        var result = userManager.ConfirmEmailAsync(user.ApplicationUser, confirmationToken).Result;
                        //here we tie the new user to the role
                        //userManager.AddToRoleAsync(serviceUser, "Admin").GetAwaiter().GetResult();
                    }
                }
            }
        }
    }

    class MyUser
    {
        public User ApplicationUser;
        public string Password;
    }
}
