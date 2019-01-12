﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using B2BApi.DbContext;
using B2BApi.Models;
using B2BApi.Models.Enum;
using B2BApi.Models.Helpers;
using B2BApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace B2BApi.Controllers
{
//    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ResultObject Get()
        {
            using (var context = new B2BDbContext())
            {
                var product = context.Products
                        .Include(price => price.Price)
                        .ToList();

                return new ResultObject
                {
                    Message = "All good",
                    Result = product
                };
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id) 
        {
            var e = new Excel();
            var r = e.Parse(id);
            return new JsonResult(r);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            
        }
    }
}