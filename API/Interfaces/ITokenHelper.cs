using API.Models;

namespace API.Interfaces
{
    public interface ITokenHelper
    {
        Task<string> CreateToken(User user);
    }
}
