using System.Threading.Tasks;
using B2BApi.ViewModels;

namespace B2BApi.Intrefaces
{
    public interface IHandlerService
    {
        Task<ServiceResult> Start(int handlerId);
    }
}