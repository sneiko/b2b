using System.Collections.Generic;
using System.Threading.Tasks;
using B2BApi.Models;

namespace B2BApi.Intrefaces
{
    public interface IHandlerRepository : IRepository
    {
        Task<Handler> GetHandlerAsync(int handlerId);
    }
}