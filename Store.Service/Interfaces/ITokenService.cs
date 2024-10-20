using Store.Data.Entities.Identity;

namespace Store.Service.Interfaces
{
    public interface ITokenService
    {
        string GenerateUserToken(AppUser user);
    }
}
