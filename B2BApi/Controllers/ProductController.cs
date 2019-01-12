using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using B2BApi.DbContext;
using B2BApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace B2BApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
         /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
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
                    var products = context.Products
                        .Include(p => p.Brand)
                        .Include(p => p.Category)
                        .Include(p => p.Price)
                        .Include(p => p.Stocks)
                        .Include(p => p.BrandType)
                        .ToList();

                    return new ResultObject
                    {
                        Result = products
                    };
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// Get product by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return product object by ID</returns>
        /// <response code="200">Product data</response>
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
                    var product = context.Products
                        .Include(p => p.Brand)
                        .Include(p => p.Attribute)
                        .Include(p => p.Category)
                        .Include(p => p.Price)
                        .Include(p => p.Stocks)
                        .Include(p => p.BrandType)
                        .Include(p => p.CompetitorsPrices)
                        .Include(p => p.CompetitorsUri)
                        .Single(i => i.Id == id);

                    return new ResultObject
                    {
                        Result = product
                    };
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Mass add new product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Task status</returns>
        /// <response code="200">Item is update</response>
        /// <response code="400">If the item is null</response> 
        [HttpPost("AddMass")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> MassAdd(List<Product> products)
        {
            try
            {
                using (var context = new B2BDbContext())
                {
                    products.ForEach(product =>
                    {
                        context.Products.Add(product);    
                    });
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
        /// Update product data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns>Task status</returns>
        /// <response code="200">Item is update</response>
        /// <response code="400">If the item is null</response> 
        /// <response code="404">Invalid product ID</response> 
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Put(int id, Product product)
        {
            try
            {
                if (id == product.Id)
                {
                    using (var context = new B2BDbContext())
                    {
                        // todo: затестить обновляются ли данные
                        context.Products.Update(product);
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
        /// Delete product
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status</returns>
        /// <response code="200">Item is update</response>
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
                    var product = context.Products.Single(i => i.Id == id);
                    context.Remove(product);
                    await context.SaveChangesAsync();
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}