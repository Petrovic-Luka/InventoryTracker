using InventoryTracker.BusinessLogic.Interfaces;
using InventoryTracker.DataAccess.Interfaces;
using InventoryTracker.Domain;

namespace InventoryTracker.BusinessLogic
{
    public class EmployeeLogic : IEmployeeLogic
    {
        IEmployeeRepository repository;

        public EmployeeLogic(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        public async Task CreateEmployeeAsync(Employee employee)
        {
            await repository.CreateEmployeeAsync(employee);
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await repository.GetAllEmployeesAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid id)
        {
            return await repository.GetEmployeeById(id);
        }
        public async Task DeleteEmployeeAsync(Guid id)
        {
            await repository.DeleteEmployee(id);
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await repository.UpdateEmployee(employee);
        }

    }
}
