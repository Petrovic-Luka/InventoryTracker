﻿using InventoryTracker.DataAccess.Enums;
using InventoryTracker.DataAccess.Interfaces;
using InventoryTracker.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace InventoryTracker.DataAccess.SQL
{
    public class BorrowSQLRepository : IBorrowRepository
    {
        IConfiguration _config;
        ILogger _logger;

        public BorrowSQLRepository(IConfiguration configuration, ILogger<BorrowSQLRepository> logger)
        {
            _config = configuration;
            _logger = logger;
        }

        public async Task CreateBorrow(Borrow borrow)
        {
            SqlTransaction transaction = null;
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {

                try
                {
                    await connection.OpenAsync();
                    transaction = connection.BeginTransaction();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.Transaction = transaction;

                    //check if equipment inventoryMark is valid
                    cmd.CommandText = "Select count(*) from Equipment where EquipmentId=@EquipmentId and status=0";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EquipmentId", borrow.EquipmentId);
                    var result = (Int32)(await cmd.ExecuteScalarAsync());
                    if (result != 1)
                    {
                        throw new ArgumentException("Equipment is not available");
                    }

                    //check if equipment is taken
                    cmd.CommandText = "Select count(*) from Equipment where EquipmentId=@EquipmentId and status=0";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EquipmentId", borrow.EquipmentId);
                    result = (Int32)(await cmd.ExecuteScalarAsync());
                    if (result != 1)
                    {
                        throw new ArgumentException("Equipment is not available");
                    }

                    //update equipment status
                    cmd.CommandText = "Update Equipment set Status=1 where EquipmentId=@EquipmentId";
                    result = await cmd.ExecuteNonQueryAsync();
                    if (result != 1)
                    {
                        throw new ArgumentException("Status update failed");
                    }

                    cmd.CommandText = "Insert into Borrow values (@EquipmentId,@EmployeeId,@StartDate,@ClassRoomId,NULL)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EmployeeId", borrow.EmployeeId);
                    cmd.Parameters.AddWithValue("@EquipmentId", borrow.EquipmentId);
                    cmd.Parameters.AddWithValue("@StartDate", DateTime.Today);
                    cmd.Parameters.AddWithValue("@ClassRoomId", borrow.ClassRoomId);
                    var output = await cmd.ExecuteNonQueryAsync();
                    if (output == 0)
                    {
                        throw new ArgumentException("Insertion failed");
                    }
                    await transaction.CommitAsync();
                }
                //Equipment cant be borrowed 2 times by same person on same day
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("Violation of PRIMARY KEY"))
                    {
                        transaction?.Rollback();
                        throw new ArgumentException("Borrow failed check if you already borrowed equipment today");

                    }
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    _logger.LogError(ex.Message);
                    throw;
                }
            }
        }

        public async Task<List<Borrow>> GetBorrowsByFilter(Guid id, BorrowSearch criteria, bool active)
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {

                try
                {
                    var output = new List<Borrow>();
                    await connection.OpenAsync();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = $"SELECT [borrow].[EquipmentId],[borrow].[EmployeeId],[StartDate],[ClassRoomId],[EndDate],e.MailAddress,eq.Description,eq.InventoryMark FROM [InventoryTrackerDB].[dbo].[Borrow] join Employee e on (e.EmployeeId=borrow.EmployeeId) join Equipment eq on (eq.EquipmentId=Borrow.EquipmentId) where borrow.{criteria}=@{criteria}";
                    if (active)
                    {
                        cmd.CommandText += " and EndDate is Null";
                    }
                    cmd.CommandText += " order by [StartDate] desc";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue($"@{criteria}", id);
                    var reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        var borrow = new Borrow();
                        borrow.EquipmentId = Guid.Parse(reader.GetString(0));
                        borrow.EmployeeId = Guid.Parse(reader.GetString(1));
                        borrow.StartDate = reader.GetDateTime(2);
                        borrow.ClassRoomId = Guid.Parse(reader.GetString(3));
                        if (!reader.IsDBNull(4))
                        {
                            borrow.EndDate = reader.GetDateTime(4);
                        }
                        borrow.Employee = new Employee
                        {
                            EmployeeId = id,
                            MailAddress = reader.GetString(5),
                        };
                        borrow.Equipment = new Equipment
                        {
                            EquipmentId = borrow.EquipmentId,
                            Description = reader.GetString(6),
                            InventoryMark = reader.GetString(7),
                        };
                        output.Add(borrow);
                    }
                    return output;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw;
                }
            }
        }

        public async Task ReturnBorrow(Borrow borrow)
        {
            SqlTransaction transaction = null;
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {

                try
                {
                    await connection.OpenAsync();
                    transaction = connection.BeginTransaction();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.Transaction = transaction;

                    //update equipment status
                    cmd.CommandText = "Update Equipment set Status=0 where EquipmentId=@EquipmentId";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EquipmentId", borrow.EquipmentId);
                    var result = await cmd.ExecuteNonQueryAsync();
                    if (result != 1)
                    {
                        throw new ArgumentException("Status update failed equipment not found");
                    }

                    cmd.CommandText = "Update Borrow set EndDate=@EndDate where EquipmentId=@EquipmentId and EmployeeId=@EmployeeId and EndDate is NULL";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EmployeeId", borrow.EmployeeId);
                    cmd.Parameters.AddWithValue("@EquipmentId", borrow.EquipmentId);
                    cmd.Parameters.AddWithValue("@EndDate", DateTime.Today);
                    var output = await cmd.ExecuteNonQueryAsync();
                    if (output == 0)
                    {
                        throw new ArgumentException("Update failed borrow not found");
                    }
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    _logger.LogError(ex.Message);
                    throw;
                }
            }
        }
    }
}
