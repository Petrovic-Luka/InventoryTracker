using InventoryTracker.Domain;

namespace InventoryTracker.DataAccess.Interfaces
{
    public interface IEmployeeRepository
    {
        Task CreateEmployeeAsync(Employee employee);
        Task DeleteEmployee(Guid id);
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee?> GetEmployeeById(Guid id);
        Task<Employee> GetEmployeeByMailAddress(string email);
        Task UpdateEmployee(Employee employee);
    }
}