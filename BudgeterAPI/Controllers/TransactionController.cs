using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using BudgeterAPI.Helpers;
using BudgeterShared.DTOs;
using BudgeterShared.Types;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BudgeterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        string connectionString;

        public TransactionController(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString(Database.BudgeterDB);
        }

        // GET: api/<TransactionController>
        [HttpGet]
        public IEnumerable<TransactionDTO> Get()
        {
            List<TransactionDTO> expenses = new List<TransactionDTO>();

            IEnumerable<TransactionDTO> rtn = Enumerable.Empty<TransactionDTO>();

            //TODO figure out how to get rid of the string
            string testCommand = "select * from WantExpense";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(testCommand, connection))
                    {
                        connection.Open();
                        using(SqlDataReader dataReader = command.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    TransactionDTO temp = new TransactionDTO()
                                    {
                                        Type = (string)dataReader[nameof(TransactionDTO.Type)],
                                        PurchaseDate = (DateTime)dataReader[nameof(TransactionDTO.PurchaseDate)],
                                        Description = (string)dataReader[nameof(TransactionDTO.Description)],
                                        Amount = (decimal)dataReader[nameof(TransactionDTO.Amount)],
                                        CreatedDate = (DateTime)dataReader[nameof(TransactionDTO.CreatedDate)],
                                        RevisionDate = (DateTime)dataReader[nameof(TransactionDTO.RevisionDate)]
                                    };
                                    
                                    expenses.Add(temp);
                                }
                            }
                        }
                    }
                }
                rtn = expenses;
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
        public void Post([FromBody] TransactionDTO transaction)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("InsertTransaction", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue(nameof(transaction.Type), transaction.Type);
                        command.Parameters.AddWithValue(nameof(transaction.PurchaseDate), transaction.PurchaseDate);
                        command.Parameters.AddWithValue(nameof(transaction.Description), transaction.Description);
                        command.Parameters.AddWithValue(nameof(transaction.Amount), transaction.Amount);
                        
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        Debug.WriteLine($"Rows Affected: {rowsAffected}");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception: {e.Message}");
            }

            Debug.WriteLine(transaction.Description);
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
