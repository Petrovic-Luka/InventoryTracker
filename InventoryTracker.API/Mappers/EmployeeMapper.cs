using AutoMapper;
using InventoryTracker.Domain;
using InventoryTrackerDTO;

namespace InventoryTracker.API.Mappers
{
    public class EmployeeMapper: Profile
    {
        public EmployeeMapper()
        {
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
        }


    }
}
