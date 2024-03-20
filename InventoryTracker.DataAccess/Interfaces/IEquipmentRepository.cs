using InventoryTracker.Domain;
using InventoryTracker.Domain.Enums;

namespace InventoryTracker.DataAccess.Interfaces
{
    public interface IEquipmentRepository
    {
        public Task CreateEquipment(Equipment equipment);
        public Task UpdateEquipment(Equipment equipment);
        public Task<List<Equipment>> GetAllEquipment();
        public Task<Equipment> GetEquipmentById(Guid id);
        public Task<List<Equipment>> GetEquipmentByType(int typeId, bool available);
    }
}
