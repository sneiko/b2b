using System.Threading.Tasks;
using B2BApi.Models;
using B2BApi.ViewModels;

namespace B2BApi.Intrefaces
{
    public interface IUsersRepository : IRepository
    {
        Task<string> GetUserSaltAsync(string username);
        Task<User> GetUserAsync(string username, string password);
        Task SaveCompleteToken(int userId, CompleteToken token);
    }
}