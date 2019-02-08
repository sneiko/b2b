using System.Threading.Tasks;
using B2BApi.FormModels;
using B2BApi.Models;
using B2BApi.ViewModels;

namespace B2BApi.Interfaces
{
    public interface IAuthorizService
    {
        Task<ServiceResult<User>> AuthorizeAsync(AuthorizeFormModel formModel);
        
        Task<ServiceResult<CompleteToken>> SaveRefreshTokenCommonAsync(User user, CompleteToken token);
        
        Task<ServiceResult<User>> ValidateRefreshTokenCommonAsync(string token, int userId);
    }
}