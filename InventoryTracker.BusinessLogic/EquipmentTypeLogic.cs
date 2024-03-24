using InventoryTracker.BusinessLogic.Interfaces;
using InventoryTracker.DataAccess.Interfaces;
using InventoryTracker.Domain;

namespace InventoryTracker.BusinessLogic
{
    public class EquipmentTypeLogic : IEquipmentTypeLogic
    {
        IEquipmentTypeRepository repository;

        public EquipmentTypeLogic(IEquipmentTypeRepository repo)
        {
            repository = repo;
        }
        public async Task<List<EquipmentType>> GetEquipmentTypes()
        {
            return await repository.GetEquipmentTypes();
        }
    }
}
