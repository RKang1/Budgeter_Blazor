using BudgeterAPI.Helpers;
using BudgeterShared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RKSQL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BudgeterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TransactionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/<TransactionController>
        [HttpGet]
        public IEnumerable<TransactionDTO> Get()
        {
            IEnumerable<TransactionDTO> rtn = Enumerable.Empty<TransactionDTO>();

            string testConnection = _configuration.GetConnectionString(Database.BudgeterDB);
            string testCommand = "select * from WantExpense";

            Query test = new Query()
            {
                CommandType = CommandTypes.Select,
                Table = Tables.WantExpense,
                Columns = new List<string>() { "*" }
            };
            Debug.WriteLine(test.PrintQuery);

            try
            {
                using (SqlConnection connection = new SqlConnection(testConnection))
                {
                    using (SqlCommand command = new SqlCommand(testCommand, connection))
                    {
                        connection.Open();
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    Debug.WriteLine($"{dataReader[0]}   {dataReader[1]}");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception: {e.Message}");
            }

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
