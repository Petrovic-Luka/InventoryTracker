using InventoryTracker.Domain;

namespace InventoryTracker.BusinessLogic.Interfaces
{
    public interface IEquipmentTypeLogic
    {
        public  Task<List<EquipmentType>> GetEquipmentTypes();
    }
}
