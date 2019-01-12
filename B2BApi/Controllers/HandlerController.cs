using System.Linq;
using B2BApi.DbContext;
using B2BApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace B2BApi.Controllers
{
    public class HandlerController : Controller
    {
        [HttpGet]
        public ResultObject Get()
        {
            using (var context = new B2BDbContext())
            {
                var handlers = context.Handlers
                    .Include(p => p.PriceColumnItems)
                    .Include(s => s.PriceColumnItems)
                    .Include(p => p.Provider)
                    .ToList();
                
                return new ResultObject
                {
                    Result = handlers
                };

            }
        }
    }
}