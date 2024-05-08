using InventoryTracker.Domain;
using InventoryTracker.Domain.Enums;

namespace InventoryTracker.BusinessLogic.Interfaces
{
    public interface IEquipmentLogic
    {
        public Task CreateEquipment(Equipment equipment);

        public Task UpdateEquipment(Equipment equipment);
        public Task<List<Equipment>> GetAllEquipment();
        public Task<Equipment> GetEquipmentById(Guid id);

        public Task RetireEquipment(Equipment equipment);
        public Task<List<Equipment>> GetEquipmentByType(int typeId, bool available);
        public Task<Equipment> GetEquipmentByInventoryMark(string mark);

    }
}
