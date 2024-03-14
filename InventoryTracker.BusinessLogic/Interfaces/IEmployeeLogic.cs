using InventoryTracker.Domain;

namespace InventoryTracker.BusinessLogic.Interfaces
{
    public interface IEmployeeLogic
    {
        Task CreateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(Guid id);
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(Guid id);
        Task UpdateEmployeeAsync(Employee employee);
    }
}