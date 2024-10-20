using Microsoft.AspNetCore.Identity;
using System.Net.Sockets;

namespace Store.Data.Entities.Identity
{
    public class AppUser :IdentityUser
    {
        public string DisplayName { get; set; }
        public Address Address { get; set; }
    }
}
