using AutoMapper;
using InventoryTracker.Domain;
using InventoryTrackerDTO.Employee;

namespace InventoryTracker.API.Mappers
{
    public class EmployeeMapper: Profile
    {
        public EmployeeMapper()
        {
            CreateMap<Employee, CreateEmployeeDTO>().ReverseMap();
        }


    }
}
