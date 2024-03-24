using InventoryTracker.DataAccess.Interfaces;
using InventoryTracker.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace InventoryTracker.DataAccess.SQL
{
    public class ClassRoomSQLRepository : IClassRoomRepository
    {
        IConfiguration _config;
        ILogger _logger;
        public ClassRoomSQLRepository(IConfiguration config,ILogger<ClassRoomSQLRepository> logger)
        {
            _config= config;
            _logger = logger;
        }

        public async Task<List<ClassRoom>> GetAllClassRooms()
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {

                try
                {
                    var output = new List<ClassRoom>();
                    await connection.OpenAsync();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "select [ClassRoomId],[Name] FROM [InventoryTrackerDB].[dbo].[ClassRoom]";
                    var reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        var classRoom = new ClassRoom();
                        classRoom.ClassRoomId = Guid.Parse(reader.GetString(0));
                        classRoom.Name = reader.GetString(1);
                        output.Add(classRoom);
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
