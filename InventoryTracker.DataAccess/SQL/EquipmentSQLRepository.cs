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
                        throw new Exception("Insertion failed");
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

        public Task<Equipment> GetEquipmentById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Equipment>> GetEquipmentByType(Guid typeId, EquipmentStatus status)
        {
            throw new NotImplementedException();
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
