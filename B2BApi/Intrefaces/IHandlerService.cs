using System.Threading.Tasks;
using B2BApi.Models;
using B2BApi.ViewModels;

namespace B2BApi.Intrefaces
{
    public interface IHandlerService
    {
        Task<ServiceResult> Start(int handlerId);
        Task<ServiceResult<Handler>> GetHandlerAsync(int handlerId);
    }
}