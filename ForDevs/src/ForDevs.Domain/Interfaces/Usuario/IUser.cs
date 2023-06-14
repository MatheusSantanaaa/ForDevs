using System.Security.Claims;

namespace ForDevs.Domain.Interfaces.Usuario
{
    public interface IUser
    {
        string Name { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
