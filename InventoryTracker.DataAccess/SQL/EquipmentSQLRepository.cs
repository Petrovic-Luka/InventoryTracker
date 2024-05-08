using InventoryTracker.DataAccess.Interfaces;
using InventoryTracker.Domain;
using InventoryTracker.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace InventoryTracker.DataAccess.SQL
{
    public class EquipmentSQLRepository : IEquipmentRepository
    {
        IConfiguration _config;
        ILogger _logger;

        public EquipmentSQLRepository(IConfiguration configuration, ILogger<EquipmentSQLRepository> logger)
        {
            _config = configuration;
            _logger = logger;
        }

        public async Task CreateEquipment(Equipment equipment)
        {
            SqlTransaction transaction = null;
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {

                try
                {
                    equipment.EquipmentId = Guid.NewGuid();
                    await connection.OpenAsync();
                    transaction = connection.BeginTransaction();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.Transaction = transaction;

                    //check if exists
                    cmd.CommandText = "Select count(*) from Equipment where InventoryMark=@InventoryMark and SerialMark=@SerialMark";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@InventoryMark", equipment.InventoryMark);
                    cmd.Parameters.AddWithValue("@SerialMark", equipment.SerialMark);
                    var result = (Int32)(await cmd.ExecuteScalarAsync());
                    if (result != 0)
                    {
                        throw new ArgumentException("Equipment with given serial and inventory mark already exists");
                    }

                    cmd.CommandText = "Insert into Equipment values (@EquipmentId,@Description,@Note,@SerialMark,@InventoryMark,@EquipmentTypeId,@Status)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EquipmentId", equipment.EquipmentId);
                    cmd.Parameters.AddWithValue("@Description", equipment.Description);
                    if (equipment.Note != null)
                    {
                        cmd.Parameters.AddWithValue("@Note", equipment.Note);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Note", "");
                    }
                    cmd.Parameters.AddWithValue("@SerialMark", equipment.SerialMark);
                    cmd.Parameters.AddWithValue("@InventoryMark", equipment.InventoryMark);
                    cmd.Parameters.AddWithValue("@EquipmentTypeId", equipment.EquipmentTypeId);
                    cmd.Parameters.AddWithValue("@Status", 0);
                    var output = await cmd.ExecuteNonQueryAsync();
                    if (output == 0)
                    {
                        throw new ArgumentException("Insertion failed");
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

        public async Task<List<Equipment>> GetAllEquipment()
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {

                try
                {
                    var output = new List<Equipment>();
                    await connection.OpenAsync();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "select [EquipmentId],[Description],[Note],[SerialMark],[InventoryMark],[EquipmentTypeId],[Status] FROM [InventoryTrackerDB].[dbo].[Equipment]";
                    var reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        var equipment = new Equipment();
                        equipment.EquipmentId = Guid.Parse(reader.GetString(0));
                        equipment.Description = reader.GetString(1);
                        equipment.Note = reader.GetString(2);
                        equipment.SerialMark = reader.GetString(3);
                        equipment.InventoryMark = reader.GetString(4);
                        equipment.EquipmentTypeId = reader.GetInt32(5);
                        equipment.EquipmentStatus = (EquipmentStatus)reader.GetInt32(6);
                        output.Add(equipment);
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

        public async Task RetireEquipment(Equipment equipment)
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

                    //check if equipment is taken
                    cmd.CommandText = "SELECT [Status] FROM [InventoryTrackerDB].[dbo].[Equipment] where EquipmentId=@EquipmentId";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EquipmentId", equipment.EquipmentId);
                    var currentStatus = await cmd.ExecuteScalarAsync();
                    if (currentStatus == null)
                    {
                        throw new ArgumentException("Equipment not found");
                    }

                    int currentStatusValue = (int)currentStatus;

                    if (currentStatusValue == 1 || currentStatusValue == 3)
                    {
                        throw new ArgumentException("Status change not possible");
                    }

                    if (currentStatusValue == 2 && equipment.EquipmentStatus != EquipmentStatus.Expended)
                    {
                        throw new ArgumentException("Status change not possible");
                    }

                    //update equipment status
                    cmd.CommandText = "Update Equipment set Status=@Status where EquipmentId=@EquipmentId";
                    cmd.Parameters.AddWithValue("@Status", equipment.EquipmentStatus);
                    var result = await cmd.ExecuteNonQueryAsync();
                    if (result != 1)
                    {
                        throw new ArgumentException("Status update failed");
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

        public async Task<Equipment> GetEquipmentById(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {

                try
                {
                    Equipment output = null;
                    await connection.OpenAsync();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "select [EquipmentId],[Description],[Note],[SerialMark],[InventoryMark],[EquipmentTypeId],[Status] FROM [InventoryTrackerDB].[dbo].[Equipment] where equipmentId = @equipmentId";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@equipmentId", id);
                    var reader = await cmd.ExecuteReaderAsync();
                    if (reader.Read())
                    {
                        output = new Equipment();
                        output.EquipmentId = Guid.Parse(reader.GetString(0));
                        output.Description = reader.GetString(1);
                        output.Note = reader.GetString(2);
                        output.SerialMark = reader.GetString(3);
                        output.InventoryMark = reader.GetString(4);
                        output.EquipmentTypeId = reader.GetInt32(5);
                        output.EquipmentStatus = (EquipmentStatus)reader.GetInt32(6);
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

        public async Task<Equipment> GetEquipmentByInventoryMark(string mark)
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {

                try
                {
                    Equipment output = null;
                    await connection.OpenAsync();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "select [EquipmentId],[Description],[Note],[SerialMark],[InventoryMark],[EquipmentTypeId],[Status] FROM [InventoryTrackerDB].[dbo].[Equipment] where InventoryMark = @InventoryMark";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@InventoryMark", mark);
                    var reader = await cmd.ExecuteReaderAsync();
                    if (reader.Read())
                    {
                        output = new Equipment();
                        output.EquipmentId = Guid.Parse(reader.GetString(0));
                        output.Description = reader.GetString(1);
                        output.Note = reader.GetString(2);
                        output.SerialMark = reader.GetString(3);
                        output.InventoryMark = reader.GetString(4);
                        output.EquipmentTypeId = reader.GetInt32(5);
                        output.EquipmentStatus = (EquipmentStatus)reader.GetInt32(6);
                    }
                    else
                    {
                        throw new ArgumentException("Equipment not found");
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

        public async Task<List<Equipment>> GetEquipmentByType(int typeId, bool available)
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {

                try
                {
                    var output = new List<Equipment>();
                    await connection.OpenAsync();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "select [EquipmentId],[Description],[Note],[SerialMark],[InventoryMark],[EquipmentTypeId],[Status] FROM [InventoryTrackerDB].[dbo].[Equipment] where EquipmentTypeId=@EquipmentTypeId";
                    if (available)
                    {
                        cmd.CommandText += " and status=0";
                    }
                    cmd.CommandText += "  order by [Description]";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EquipmentTypeId", typeId);
                    var reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        var equipment = new Equipment();
                        equipment.EquipmentId = Guid.Parse(reader.GetString(0));
                        equipment.Description = reader.GetString(1);
                        equipment.Note = reader.GetString(2);
                        equipment.SerialMark = reader.GetString(3);
                        equipment.InventoryMark = reader.GetString(4);
                        equipment.EquipmentTypeId = reader.GetInt32(5);
                        equipment.EquipmentStatus = (EquipmentStatus)reader.GetInt32(6);
                        output.Add(equipment);
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



        public Task UpdateEquipment()
        {
            throw new NotImplementedException();
        }

        public Task UpdateEquipment(Equipment equipment)
        {
            throw new NotImplementedException();
        }
    }
}
