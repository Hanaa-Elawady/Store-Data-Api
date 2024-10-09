using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.Identity;

namespace Store.Repository
{
    public class StoreIdentityContexySeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Hanaa El-Awady",
                    Email = "hanaa.m.elawady@gmail.com",
                    UserName = "Hanaa",
                    Address = new Address
                    {
                        FirstName = "Hanaa",
                        LastName = "El-Awady",
                        City = "Cairo",
                        State = "Cairo",
                        Street = "32",
                        PostalCode = "2464"
                    }
                };
                await userManager.CreateAsync(user , "Password123!");
            }
        }
    }
}
