using InventoryTracker.DataAccess.Interfaces;
using InventoryTracker.Domain;
using InventoryTracker.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace InventoryTracker.DataAccess.SQL
{
    public class EmployeeSQLRepository : IEmployeeRepository
    {
        IConfiguration _config;
        ILogger _logger;

        public EmployeeSQLRepository(IConfiguration configuration, ILogger<EmployeeSQLRepository> logger)
        {
            _config = configuration;
            _logger = logger;
        }

        public async Task CreateEmployeeAsync(Employee employee)
        {
            SqlTransaction transaction = null;
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {

                try
                {
                    employee.EmployeeId = Guid.NewGuid();
                    await connection.OpenAsync();
                    transaction = connection.BeginTransaction();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.Transaction = transaction;
                    cmd.CommandText = "Insert into Employee values (@EmployeeId,@FirstName,@LastName,@MailAddress)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                    cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@MailAddress", employee.MailAddress);
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

        public Task DeleteEmployee(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {

                try
                {
                    var output = new List<Employee>();
                    await connection.OpenAsync();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "select [EmployeeId],[FirstName],[LastName],[MailAddress] FROM [InventoryTrackerDB].[dbo].[Employee]";
                    var reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        var employee = new Employee();
                        employee.EmployeeId = Guid.Parse(reader.GetString(0));
                        employee.FirstName = reader.GetString(1);
                        employee.LastName = reader.GetString(2);
                        employee.MailAddress = reader.GetString(3);
                        output.Add(employee);
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

        public async Task<Employee?> GetEmployeeById(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "select [EmployeeId],[FirstName],[LastName],[MailAddress] FROM [InventoryTrackerDB].[dbo].[Employee] where EmployeeId=@EmployeeId";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EmployeeId", id);
                    var reader = await cmd.ExecuteReaderAsync();
                    if (reader.Read())
                    {
                        var employee = new Employee();
                        employee.EmployeeId = Guid.Parse(reader.GetString(0));
                        employee.FirstName = reader.GetString(1);
                        employee.LastName = reader.GetString(2);
                        employee.MailAddress = reader.GetString(3);
                        return employee;
                    }
                    else
                    {
                        throw new ArgumentException("User not found");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw;
                }
            }
        }

        public async Task<Employee?> GetEmployeeByMailAddress(string email)
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "select [EmployeeId],[FirstName],[LastName],[MailAddress] FROM [InventoryTrackerDB].[dbo].[Employee] where MailAddress=@MailAddress";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@MailAddress", email);
                    var reader = await cmd.ExecuteReaderAsync();
                    if (reader.Read())
                    {
                        var employee = new Employee();
                        employee.EmployeeId = Guid.Parse(reader.GetString(0));
                        employee.FirstName = reader.GetString(1);
                        employee.LastName = reader.GetString(2);
                        employee.MailAddress = reader.GetString(3);
                        return employee;
                    }
                    else
                    {
                        throw new ArgumentException("User not found");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw;
                }
            }
        }

        public Task UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
