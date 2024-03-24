using InventoryTracker.Domain;

namespace InventoryTracker.BusinessLogic.Interfaces
{
    public interface IClassRoomLogic
    {
        public Task<List<ClassRoom>> GetAllClassRooms();
    }
}
