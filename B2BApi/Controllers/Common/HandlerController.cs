using System;
using System.Linq;
using System.Threading.Tasks;
using B2BApi.DbContext;
using B2BApi.Intrefaces;
using B2BApi.Models;
using B2BApi.Models.Enum;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace B2BApi.Controllers
{
    [Area("Admin")]
    [Route("api/v1/[area]/[controller]")]
    [Authorize(Roles = "Admin, Manager, Director")]
    public class HandlerController : Controller
    {   
        private readonly IHandlerService _handlerService;

        public HandlerController(IHandlerService handlerService)
        {
            _handlerService = handlerService;
        }

        /// <summary>
        /// Get all handlers
        /// </summary>
        /// <returns>Handler list array</returns>
        /// <response code="200">Handler List</response>
        /// <response code="400">If the items is null</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<ResultObject> Get()
        {
            try
            {
                using (var context = new B2BDbContext())
                {
                    var handlers = context.Handlers.ToList();

                    return new ResultObject
                    {
                        Result = handlers
                    };
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// Get handler by ID
        /// </summary>
        /// <param name="id">Handler ID</param>
        /// <returns>Return handler object by ID</returns>
        /// <response code="200">Handler data</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<ResultObject> Get(int id)
        {
            try
            {
                using (var context = new B2BDbContext())
                {
                    var handler = context.Handlers
                        .Include(p => p.GrabColumnItems)
                        .Include(s => s.GrabColumnItems)
                        .Include(p => p.Provider)
                        .Single(i => i.Id == id);

                    return new ResultObject
                    {
                        Result = handler
                    };
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Add new handler
        /// </summary>
        /// <param name="handler">Handler object</param>
        /// <returns>Task status</returns>
        /// <response code="200">Item is update</response>
        /// <response code="400">If the item is null</response> 
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Post(Handler handler)
        {
            try
            {
                using (var context = new B2BDbContext())
                {
                    context.Handlers.Add(handler);
                    await context.SaveChangesAsync();
                    
                    // Set task by timeSpan from handler
                    BackgroundJob.Schedule(
                        () => Patch(handler.Id),
                        handler.HandlerScheduler);
                    
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Update handler data
        /// </summary>
        /// <param name="id">Handler ID</param>
        /// <param name="handler">New handler object</param>
        /// <returns>Task status</returns>
        /// <response code="200">Item is update</response>
        /// <response code="400">If the item is null</response> 
        /// <response code="404">Invalid handler ID</response> 
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Put(int id, Handler handler)
        {
            try
            {
                if (id == handler.Id)
                {
                    using (var context = new B2BDbContext())
                    {
                        // Check if work
                        HandlerStatus status = context.Handlers.Single(i => i.Id == id).Status;
                        if (status == HandlerStatus.Work) return Conflict("Handler in work!");
                        
                        // Set task by timeSpan from handler
                        BackgroundJob.Schedule(
                            () => Patch(handler.Id),
                            handler.HandlerScheduler);
                        
                        // todo: затестить обновляются ли данные
                        context.Handlers.Update(handler);
                        await context.SaveChangesAsync();
                        return Ok();
                    }
                }
                else
                {
                    return NotFound("Invalid handler 'Id' field");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Delete handler by ID
        /// </summary>
        /// <param name="id">Handler ID</param>
        /// <response code="200">Item is delete</response>
        /// <response code="400">If the item is null</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                using (var context = new B2BDbContext())
                {
                    var handler = context.Handlers.Single(i => i.Id == id);
                    context.Remove(handler);
                    await context.SaveChangesAsync();
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        
        /// <summary>
        /// Start handler by ID
        /// </summary>
        /// <param name="id">Handler ID</param>
        /// <response code="200">Handler started</response>
        /// <response code="400">If the item is null</response> 
        [HttpPatch("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Patch(int id)
        {
            return Ok(await _handlerService.Start(id));
        }
    }
}