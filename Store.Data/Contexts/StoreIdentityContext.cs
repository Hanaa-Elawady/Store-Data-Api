using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.Data.Entities.Identity;

namespace Store.Data.Contexts
{
    public class StoreIdentityContext : IdentityDbContext<AppUser>
    {
        public StoreIdentityContext(DbContextOptions<StoreIdentityContext> options) : base(options)
        {
        }
    }
}
