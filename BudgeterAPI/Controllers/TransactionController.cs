using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BudgeterShared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BudgeterAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        // GET: api/<TransactionController>
        [HttpGet]
        public IEnumerable<TransactionDTO> Get()
        {
            List<TransactionDTO> expenses = new List<TransactionDTO>();

            //This is just to provide some temporary data until the database is hooked up
            for (int i = 0; i < 10; i++)
            {
                TransactionDTO temp = new TransactionDTO()
                {
                    Date = DateTime.Now.AddDays(i + -10),
                    Description = String.Concat("Item ", (i + 1)),
                    Amount = 10 + i
                };

                expenses.Add(temp);
            }

            IEnumerable<TransactionDTO> rtn = expenses;

            return rtn;
        }

        // GET api/<TransactionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TransactionController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TransactionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TransactionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
