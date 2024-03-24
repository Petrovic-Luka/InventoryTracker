using InventoryTracker.Domain;

namespace InventoryTracker.DataAccess.Interfaces
{
    public interface IClassRoomRepository
    {
        public Task<List<ClassRoom>> GetAllClassRooms();
    }
}
