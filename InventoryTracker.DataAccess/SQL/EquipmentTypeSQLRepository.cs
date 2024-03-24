using InventoryTracker.DataAccess.Interfaces;
using InventoryTracker.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace InventoryTracker.DataAccess.SQL
{
    public class EquipmentTypeSQLRepository : IEquipmentTypeRepository
    {
        IConfiguration _config;
        ILogger _logger;
        public EquipmentTypeSQLRepository(IConfiguration config, ILogger<EquipmentTypeSQLRepository> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task<List<EquipmentType>> GetEquipmentTypes()
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {

                try
                {
                    var output = new List<EquipmentType>();
                    await connection.OpenAsync();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "select [EquipmentTypeId],[Name] FROM [InventoryTrackerDB].[dbo].[EquipmentType]";
                    var reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        var equipmentType = new EquipmentType();
                        equipmentType.EquipmentTypeId = reader.GetInt32(0);
                        equipmentType.Name = reader.GetString(1);
                        output.Add(equipmentType);
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
    }
}
