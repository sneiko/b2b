using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using B2BApi.Controllers.Base;
using B2BApi.DbContext;
using B2BApi.Helpers;
using B2BApi.Intrefaces;
using B2BApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace B2BApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin, Manager, Director")]
    public class ProductController : ControllerAuthorizeApi
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        
         /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>Product list array</returns>
        /// <response code="200">Handler List</response>
        /// <response code="400">If the items is null</response> 
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get()
        {
            if (!Request.TryGetUserId(out var userId))
            {
                return StatusCode(403, "Токен протух");
            }
            return Ok(await _productService.GetProductListAsync());
        }



        /// <summary>
        /// Get product by ID
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Return product object by ID</returns>
        /// <response code="200">Product data</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get(int id)
        {
            if (!Request.TryGetUserId(out var userId))
            {
                return StatusCode(403, "Токен протух");
            }
            return Ok(await _productService.GetProductAsync(id));
        }

        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="product">Product ID</param>
        /// <returns>Task status</returns>
        /// <response code="200">Item is update</response>
        /// <response code="400">If the item is null</response> 
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post(Product product)
        {
            if (!Request.TryGetUserId(out var userId))
            {
                return StatusCode(403, "Токен протух");
            }
            return Ok(await _productService.AddProductAsync(product));
        }

        /// <summary>
        /// Update product data
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <param name="product">New product object</param>
        /// <returns>Task status</returns>
        /// <response code="200">Item is update</response>
        /// <response code="400">If the item is null</response> 
        /// <response code="404">Invalid product ID</response> 
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put(Product product)
        {
            if (!Request.TryGetUserId(out var userId))
            {
                return StatusCode(403, "Токен протух");
            }
            return Ok(await _productService.UpdateProductAsync(product));
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Status</returns>
        /// <response code="200">Item is update</response>
        /// <response code="400">If the item is null</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Delete(int id)
        {
            if (!Request.TryGetUserId(out var userId))
            {
                return StatusCode(403, "Токен протух");
            }
            return Ok(await _productService.DeleteProductAsync(id));
        }
    }
}