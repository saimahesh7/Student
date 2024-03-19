using Microsoft.AspNetCore.Identity;

namespace Student.Repositories
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
