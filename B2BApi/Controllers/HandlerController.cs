using System.Linq;
using System.Threading.Tasks;
using B2BApi.DbContext;
using B2BApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace B2BApi.Controllers
{
    public class HandlerController : Controller
    {
        // GET api/handler
        [HttpGet]
        public ResultObject Get()
        {
            using (var context = new B2BDbContext())
            {
                var handlers = context.Handlers
//                    .Include(p => p.PriceColumnItems)
//                    .Include(s => s.PriceColumnItems)
                    .Include(p => p.Provider)
                    .ToList();
                
                return new ResultObject
                {
                    Result = handlers
                };
            }
        }


        // GET api/handler/{id}
        [HttpGet("{id}")]
        public ResultObject Get(int id)
        {
            using (var context = new B2BDbContext())
            {
                var handler = context.Handlers
                    .Include(p => p.PriceColumnItems)
                    .Include(s => s.PriceColumnItems)
                    .Include( p => p.Provider)
                    .Single(i => i.Id == id);
                
                return new ResultObject
                {
                    Result = handler
                };
            }
        }

        // POST api/handler
        [HttpPost]
        public void Post([FromBody] Handler handler)
        {
            using (var context = new B2BDbContext())
            {
                context.Add(handler);
                context.SaveChanges();
            }
        }

        // PUT api/handler/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Handler handler)
        {
            if (id == handler.Id)
            {
                using (var context = new B2BDbContext())
                {
                    // todo: затестить обновляются ли данные
                    context.Handlers.Update(handler);
                    context.SaveChanges();
                    return Ok();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/handler/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (var context = new B2BDbContext())
            {
                var handler = context.Handlers.Single(i => i.Id == id);
                context.Remove(handler);
                context.SaveChanges();
                return Ok();
            }
        }
    }
}