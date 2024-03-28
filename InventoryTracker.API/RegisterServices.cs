using InventoryTracker.API.Mappers;
using InventoryTracker.BusinessLogic;
using InventoryTracker.BusinessLogic.Interfaces;
using InventoryTracker.DataAccess.Interfaces;
using InventoryTracker.DataAccess.SQL;

namespace InventoryTracker.API
{
    public static class RegisterServices
    {
        /// <summary>
        /// Used to register all services required for dependency injection in app
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
            builder.Services.AddTransient<IEmployeeRepository, EmployeeSQLRepository>();
            builder.Services.AddTransient<IEmployeeLogic, EmployeeLogic>();

            //equipment
            builder.Services.AddTransient<IEquipmentLogic, EquipmentLogic>();
            builder.Services.AddTransient<IEquipmentRepository, EquipmentSQLRepository>();

            //borrow
            builder.Services.AddTransient<IBorrowLogic, BorrowLogic>();
            builder.Services.AddTransient<IBorrowRepository, BorrowSQLRepository>();

            //classroom
            builder.Services.AddTransient<IClassRoomLogic, ClassRoomLogic>();
            builder.Services.AddTransient<IClassRoomRepository, ClassRoomSQLRepository>();

            //equipment types
            builder.Services.AddTransient<IEquipmentTypeLogic, EquipmentTypeLogic>();
            builder.Services.AddTransient<IEquipmentTypeRepository, EquipmentTypeSQLRepository>();
        }
    }
}
