using B2BApi.DbContext;
using Microsoft.EntityFrameworkCore;

namespace B2BApi.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly B2BDbContext Context;

        protected BaseRepository(B2BDbContext context)
        {
            Context = context;
            Context.Database.SetCommandTimeout(10000);
        }
    }
}