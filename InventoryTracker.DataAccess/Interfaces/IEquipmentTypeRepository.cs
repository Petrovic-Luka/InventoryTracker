using InventoryTracker.Domain;

namespace InventoryTracker.DataAccess.Interfaces
{
    public interface IEquipmentTypeRepository
    {
        public Task<List<EquipmentType>> GetEquipmentTypes();
    }
}
