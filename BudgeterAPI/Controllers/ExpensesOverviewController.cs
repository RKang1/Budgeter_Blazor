using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BudgeterShared.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BudgeterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesOverviewController : ControllerBase
    {
        // GET: api/<ExpensesOverviewController>
        [HttpGet]
        public ExpensesOverviewDTO Get()
        {
            //This is just to provide some temporary data until the database is hooked up
            //This will be calling a stored procedure that calculates the totals
            ExpensesOverviewDTO rtn = new ExpensesOverviewDTO()
            {
                Income = 2000,
                Savings = 500,
                Needs= 500,
                Wants = 1000,
                TotalNeedsAmount = 200
            };

            return rtn;
        }

        // GET api/<ExpensesOverviewController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ExpensesOverviewController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ExpensesOverviewController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ExpensesOverviewController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
