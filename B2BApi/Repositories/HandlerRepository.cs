using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using B2BApi.DbContext;
using B2BApi.Intrefaces;
using B2BApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace B2BApi.Repositories
{
    public class HandlerRepository : BaseRepository, IHandlerRepository
    {
        public HandlerRepository(B2BDbContext context) : base(context){}

        /// <summary>
        /// Get handler from DB
        /// </summary>
        /// <param name="handlerId"></param>
        /// <returns></returns>
        public async Task<Handler> GetHandlerAsync(int handlerId)
            => await Context.Handlers.FirstOrDefaultAsync(x => x.Id == handlerId);

        /// <summary>
        /// Get handler list from DB
        /// </summary>
        /// <returns></returns>
        public async Task<List<Handler>> GetHandlerListAsync()
            => await Context.Handlers.ToListAsync();
        
        /// <summary>
        /// Delete handler from DB
        /// </summary>
        /// <param name="handlerId"></param>
        /// <returns></returns>
        public async Task DeleteHandlerAsync(int handlerId)
            => Context.Handlers.Remove(
                await Context.Handlers.FirstOrDefaultAsync(x => x.Id == handlerId)
                );

        /// <summary>
        /// Update handler in DB
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public async Task UpdateHandler(Handler handler)
            => Context.Handlers.Update(handler);

        /// <summary>
        /// Add handler to DB
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public async Task AddHandler(Handler handler)
            => Context.Handlers.Add(handler);
    }
}