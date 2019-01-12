using System;
using System.Linq;
using System.Threading.Tasks;
using B2BApi.DbContext;
using B2BApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace B2BApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandlerController : Controller
    {
        // GET api/handler
        [HttpGet]
        public ResultObject Get()
        {
            using (var context = new B2BDbContext())
            {
                var handlers = context.Handlers
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
                    .Include(p => p.GrabColumnItems)
                    .Include(s => s.GrabColumnItems)
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
        public async Task<ActionResult> PostTodoItem(Handler handler)
        {
            using (var context = new B2BDbContext())
            {
                context.Handlers.Add(handler);
                await context.SaveChangesAsync();
                return Ok();
            }
             
        }

        // PUT api/handler/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Handler handler)
        {
            if (id == handler.Id)
            {
                using (var context = new B2BDbContext())
                {
                    // todo: затестить обновляются ли данные
                    context.Handlers.Update(handler);
                    await context.SaveChangesAsync();
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
        public async Task<ActionResult> Delete(int id)
        {
            using (var context = new B2BDbContext())
            {
                var handler = context.Handlers.Single(i => i.Id == id);
                context.Remove(handler);
                await context.SaveChangesAsync();
                return Ok();
            }
        }
    }
}