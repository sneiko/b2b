using System.Collections.Generic;
using System.Threading.Tasks;
using B2BApi.Models;
using B2BApi.ViewModels;

namespace B2BApi.Interfaces
{
    public interface IHandlerService
    {
        Task<ServiceResult<Handler>> GetHandlerAsync(int handlerId);
        Task<ServiceResult<List<Handler>>> GetHandlerListAsync();
        Task<ServiceResult> DeleteHandlerAsync(int handlerId);
        Task<ServiceResult> UpdateHandlerAsync(Handler handler);
        Task<ServiceResult> AddHandlerAsync(Handler handler);
        
        
        /// <summary>
        /// Start parse from Handler price
        /// </summary>
        /// <param name="handlerId"></param>
        /// <returns></returns>
        Task<ServiceResult> Start(int handlerId);
    }
}