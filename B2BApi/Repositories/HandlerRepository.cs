using System.Threading.Tasks;
using B2BApi.DbContext;
using B2BApi.Intrefaces;
using B2BApi.Models;
using Microsoft.EntityFrameworkCore;

namespace B2BApi.Repositories
{
    public class HandlerRepository : BaseRepository, IHandlerRepository
    {
        public HandlerRepository(B2BDbContext context) : base(context)
        {
            
        }

        public async Task<Handler> GetHandlerAsync(int handlerId)
        => await Context.Handlers
                .FirstOrDefaultAsync(x => x.Id == handlerId);
        
    }
}