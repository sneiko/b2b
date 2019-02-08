using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using B2BApi.Controllers.Base;
using B2BApi.DbContext;
using B2BApi.Helpers;
using B2BApi.Interfaces;
using B2BApi.Models;
using B2BApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace B2BApi.Controllers
{
    [Area("Common")]
    [Route("api/v1/[area]/[controller]")]
    [Authorize(Roles = "Admin, Manager, Director")]
    public class HandlerController : ControllerAuthorizeApi
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
        [ProducesResponseType(typeof(ServiceResult<List<Handler>>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get()
        {
            if (!Request.TryGetUserId(out var userId))
            {
                return StatusCode(403, "Токен протух");
            }
            return Ok(await _handlerService.GetHandlerListAsync());
        }



        /// <summary>
        /// Get handler by ID
        /// </summary>
        /// <param name="id">Handler ID</param>
        /// <returns>Return handler object by ID</returns>
        /// <response code="200">Handler data</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceResult<Handler>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get(int id)
        {
            if (!Request.TryGetUserId(out var userId))
            {
                return StatusCode(403, "Токен протух");
            }
            return Ok(await _handlerService.GetHandlerAsync(id));
        }


        /// <summary>
        /// Add new handler
        /// </summary>
        /// <param name="handler">Handler object</param>
        /// <returns>Task status</returns>
        /// <response code="200">Item is update</response>
        /// <response code="400">If the item is null</response> 
        [HttpPost]
        [ProducesResponseType(typeof(ServiceResult), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post(Handler handler)
        {
            if (!Request.TryGetUserId(out var userId))
            {
                return StatusCode(403, "Токен протух");
            }
            return Ok(await _handlerService.AddHandlerAsync(handler));
        }

        /// <summary>
        /// Update handler data
        /// </summary>
        /// <param name="handler">New handler object</param>
        /// <returns>Task status</returns>
        /// <response code="200">Item is update</response>
        /// <response code="400">If the item is null</response> 
        /// <response code="404">Invalid handler ID</response> 
        [HttpPut]
        [ProducesResponseType(typeof(ServiceResult), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put(Handler handler)
        {
            if (!Request.TryGetUserId(out var userId))
            {
                return StatusCode(403, "Токен протух");
            }
            return Ok(await _handlerService.UpdateHandlerAsync(handler));
        }


        /// <summary>
        /// Delete handler by ID
        /// </summary>
        /// <param name="id">Handler ID</param>
        /// <response code="200">Item is delete</response>
        /// <response code="400">If the item is null</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ServiceResult), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Delete(int id)
        {
            if (!Request.TryGetUserId(out var userId))
            {
                return StatusCode(403, "Токен протух");
            }
            return Ok(await _handlerService.DeleteHandlerAsync(id));
        }

        /// <summary>
        /// Start handler parse by ID
        /// </summary>
        /// <param name="id">Handler ID</param>
        /// <response code="200">Item is delete</response>
        /// <response code="400">If the item is null</response> 
        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(ServiceResult), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Patch(int id)
        {
            if (!Request.TryGetUserId(out var userId))
            {
                return StatusCode(403, "Токен протух");
            }
            return Ok(await _handlerService.Start(id));
        }
    }
}