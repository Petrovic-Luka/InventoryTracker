using InventoryTracker.API.Mappers;
using InventoryTracker.BusinessLogic;
using InventoryTracker.BusinessLogic.Interfaces;
using InventoryTracker.DataAccess.InMemory;
using InventoryTracker.DataAccess.Interfaces;
using InventoryTracker.DataAccess.SQL;

namespace InventoryTracker.API
{
    public static class RegisterServices
    {
        /// <summary>
        /// Used to register all services required for dependecy injection in app
        /// </summary>
        /// <param name="builder"></param>
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            //Configurating app level services
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
            builder.Services.AddAutoMapper(typeof(EmployeeMapper).Assembly);
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            //Domain models

            //employee
            builder.Services.AddSingleton<IEmployeeRepository, EmployeeInMemoryRepository>();
            builder.Services.AddSingleton<IEmployeeLogic, EmployeeLogic>();

            //equipment
            builder.Services.AddTransient<IEquipmentLogic, EquipmentLogic>();
            builder.Services.AddTransient<IEquipmentRepository, EquipmentSQLRepository>();

        }
    }
}
