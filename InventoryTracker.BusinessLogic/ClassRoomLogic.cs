using InventoryTracker.BusinessLogic.Interfaces;
using InventoryTracker.DataAccess.Interfaces;
using InventoryTracker.Domain;

namespace InventoryTracker.BusinessLogic
{
    public class ClassRoomLogic : IClassRoomLogic
    {
        IClassRoomRepository repository;

        public ClassRoomLogic(IClassRoomRepository repo)
        {
            repository = repo;
        }
        public async Task<List<ClassRoom>> GetAllClassRooms()
        {
           return await repository.GetAllClassRooms();
        }
    }
}
