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
using BudgeterShared.Enums;
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
            string testCommand = "select * from Transactions";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using SqlCommand command = new SqlCommand(testCommand, connection);
                    connection.Open();
                    using SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            TransactionDTO temp = new TransactionDTO()
                            {
                                Id = (int)dataReader[nameof(TransactionDTO.Id)],
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
        public TransactionDTO Get(int id)
        {
            TransactionDTO rtn = new TransactionDTO();

            //TODO figure out how to get rid of the string
            string testCommand = $"select * from Transactions we where we.Id = {id}";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using SqlCommand command = new SqlCommand(testCommand, connection);
                    connection.Open();
                    using SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            rtn = new TransactionDTO()
                            {
                                Id = (int)dataReader[nameof(TransactionDTO.Id)],
                                PurchaseDate = (DateTime)dataReader[nameof(TransactionDTO.PurchaseDate)],
                                Description = (string)dataReader[nameof(TransactionDTO.Description)],
                                Amount = (decimal)dataReader[nameof(TransactionDTO.Amount)],
                                CreatedDate = (DateTime)dataReader[nameof(TransactionDTO.CreatedDate)],
                                RevisionDate = (DateTime)dataReader[nameof(TransactionDTO.RevisionDate)]
                            };
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

                        command.Parameters.AddWithValue(nameof(TransactionDTO.PurchaseDate), transaction.PurchaseDate);
                        command.Parameters.AddWithValue(nameof(TransactionDTO.Description), transaction.Description);
                        command.Parameters.AddWithValue(nameof(TransactionDTO.Amount), transaction.Amount);
                        
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
        }

        // PUT api/<TransactionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] TransactionDTO transaction)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("UpdateTransaction", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue(nameof(TransactionDTO.Id), transaction.Id);
                        command.Parameters.AddWithValue(nameof(TransactionDTO.PurchaseDate), transaction.PurchaseDate);
                        command.Parameters.AddWithValue(nameof(TransactionDTO.Description), transaction.Description);
                        command.Parameters.AddWithValue(nameof(TransactionDTO.Amount), transaction.Amount);

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
        }

        // DELETE api/<TransactionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("DeleteTransaction", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue(nameof(TransactionDTO.Id), id);

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
        }
    }
}
