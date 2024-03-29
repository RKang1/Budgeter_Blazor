﻿using System;
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
        private readonly string _connectionString;

        public TransactionController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString(Database.BudgeterDB);
        }

        // GET: api/TransactionController/?type=wants
        [HttpGet]
        public IEnumerable<TransactionDTO> Get(string type)
        {
            List<TransactionDTO> transactions = new List<TransactionDTO>();
            TransactionType typeEnum;
            IEnumerable<TransactionDTO> rtn = Enumerable.Empty<TransactionDTO>();

            Enum.TryParse<TransactionType>(type, true, out typeEnum);

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SelectTransaction", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(nameof(TransactionDTO.TransactionType), (int)typeEnum);

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
                }
                rtn = transactions;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception: {e.Message}");
            }

            return rtn;
        }

        // GET api/TransactionController/ById/?id=5
        [HttpGet("ById")]
        public TransactionDTO Get(int id)
        {
            TransactionDTO rtn = new TransactionDTO();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SelectTransactionById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(nameof(TransactionDTO.Id), id);

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
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception: {e.Message}");
            }

            return rtn;
        }

        // POST api/TransactionController
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

        // PUT api/TransactionController
        [HttpPut]
        public void Put([FromBody] TransactionDTO transaction)
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

        // DELETE api/TransactionController/?id=5
        [HttpDelete]
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
