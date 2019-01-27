using System.Threading.Tasks;
using B2BApi.FormModels;
using B2BApi.Models;
using B2BApi.ViewModels;

namespace B2BApi.Intrefaces
{
    public interface IAuthorizationService
    {
        Task<ServiceResult<User>> AuthorizeAsync(AuthorizeFormModel formModel);
        
        Task<ServiceResult<CompleteToken>> SaveRefreshTokenCommonAsync(User user, CompleteToken token);
    }
}