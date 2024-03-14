using InventoryTracker.DataAccess.Interfaces;
using InventoryTracker.Domain;

namespace InventoryTracker.DataAccess.InMemory
{
    public class EmployeeInMemoryRepository : IEmployeeRepository
    {
        public List<Employee> _database;

        public EmployeeInMemoryRepository()
        {
            _database =
            [
                new Employee
                {
                    EmployeeId = Guid.NewGuid(),
                    FirstName = "Pera",
                    LastName = "Peric",
                    MailAddress = "pp@gmail.com"
                },
                new Employee
                {
                    EmployeeId = Guid.NewGuid(),
                    FirstName = "Mika",
                    LastName = "Mikic",
                    MailAddress = "mm@gmail.com"
                },
                new Employee
                {
                    EmployeeId = Guid.NewGuid(),
                    FirstName = "Zika",
                    LastName = "Zikic",
                    MailAddress = "zz@gmail.com"
                },
            ];
        }

        public Task CreateEmployeeAsync(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid();
            _database.Add(employee);
            return Task.CompletedTask;
        }

        public Task<List<Employee>> GetAllEmployeesAsync()
        {
            return Task.FromResult(_database);
        }

        public Task<Employee?> GetEmployeeById(Guid id)
        {
            return Task.FromResult(_database.FirstOrDefault(x => x.EmployeeId == id));
        }

        public Task DeleteEmployee(Guid id)
        {
            var employee = _database.FirstOrDefault(x => x.EmployeeId == id);
            if (employee == null)
            {
                throw new ArgumentException();
            }
            _database.Remove(employee);
            return Task.CompletedTask;
        }

        public Task UpdateEmployee(Employee employee)
        {
            var temp = _database.FirstOrDefault(x => x.EmployeeId == employee.EmployeeId);
            if (temp == null)
            {
                throw new ArgumentException();
            }
            temp.FirstName = employee.FirstName;
            temp.LastName = employee.LastName;
            temp.MailAddress = employee.MailAddress;
            return Task.CompletedTask;
        }
    }
}
