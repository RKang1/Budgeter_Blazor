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
    [Route("api/[controller]/{transactionType}")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public TransactionController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString(Database.BudgeterDB);
        }

        // GET: api/<TransactionController>/transactionType
        [HttpGet]
        public IEnumerable<TransactionDTO> Get(string transactionType)
        {
            List<TransactionDTO> transactions = new List<TransactionDTO>();
            TransactionType typeEnum;
            IEnumerable<TransactionDTO> rtn = Enumerable.Empty<TransactionDTO>();

            Enum.TryParse<TransactionType>(transactionType, true, out typeEnum);

            //TODO figure out how to get rid of the string
            string testCommand = $"select * from Transactions where TransactionType = {(int)typeEnum}";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
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
                                TransactionType = (int)dataReader[nameof(TransactionDTO.TransactionType)],
                                PurchaseDate = (DateTime)dataReader[nameof(TransactionDTO.PurchaseDate)],
                                Description = (string)dataReader[nameof(TransactionDTO.Description)],
                                Amount = (decimal)dataReader[nameof(TransactionDTO.Amount)],
                                CreatedDate = (DateTime)dataReader[nameof(TransactionDTO.CreatedDate)],
                                RevisionDate = (DateTime)dataReader[nameof(TransactionDTO.RevisionDate)]
                            };

                            transactions.Add(temp);
                        }
                    }
                }
                rtn = transactions;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception: {e.Message}");
            }

            return rtn;
        }

        //TODO change this to get the id from the query
        // GET api/<TransactionController>/5
        [HttpGet("{id}")]
        public TransactionDTO Get(int id)
        {
            TransactionDTO rtn = new TransactionDTO();

            //TODO figure out how to get rid of the string
            string testCommand = $"select * from Transactions we where we.Id = {id}";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
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

        // POST api/<TransactionController>/transactionType
        [HttpPost]
        public void Post([FromBody] TransactionDTO transaction)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("InsertTransaction", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue(nameof(TransactionDTO.TransactionType), transaction.TransactionType);
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

        //TODO change this to get the id from the query
        // PUT api/<TransactionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] TransactionDTO transaction)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
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

        //TODO change this to get the id from the query
        // DELETE api/<TransactionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
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
