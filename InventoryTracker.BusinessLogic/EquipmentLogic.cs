using InventoryTracker.BusinessLogic.Interfaces;
using InventoryTracker.DataAccess.Interfaces;
using InventoryTracker.Domain;

namespace InventoryTracker.BusinessLogic
{

    public class EquipmentLogic : IEquipmentLogic
    {
        IEquipmentRepository repository;

        public EquipmentLogic(IEquipmentRepository repo)
        {
            repository = repo;
        }

        public async Task CreateEquipment(Equipment equipment)
        {
            await repository.CreateEquipment(equipment);
        }

        public async Task<List<Equipment>> GetAllEquipment()
        {
            return await repository.GetAllEquipment();
        }

        public Task<Equipment> GetEquipmentById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Equipment>> GetEquipmentByType(int typeId, bool available)
        {
            return await repository.GetEquipmentByType(typeId, available);
        }

        public Task UpdateEquipment(Equipment equipment)
        {
            throw new NotImplementedException();
        }
    }
}
