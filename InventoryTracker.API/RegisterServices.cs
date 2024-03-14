using InventoryTracker.API.Mappers;
using InventoryTracker.BusinessLogic;
using InventoryTracker.BusinessLogic.Interfaces;
using InventoryTracker.DataAccess.InMemory;
using InventoryTracker.DataAccess.Interfaces;

namespace InventoryTracker.API
{
    public static class RegisterServices
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
            builder.Services.AddAutoMapper(typeof(EmployeeMapper).Assembly);

            builder.Services.AddSingleton<IEmployeeRepository,EmployeeInMemoryRepository>();
            builder.Services.AddSingleton<IEmployeeLogic,EmployeeLogic>();

        }
    }
}
