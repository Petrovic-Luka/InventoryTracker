using InventoryTracker.BusinessLogic.Interfaces;
using InventoryTracker.DataAccess.Interfaces;
using InventoryTracker.Domain;
using InventoryTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task<List<Equipment>> GetEquipmentByType(Guid typeId, EquipmentStatus status)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEquipment()
        {
            throw new NotImplementedException();
        }

        public Task UpdateEquipment(Equipment equipment)
        {
            throw new NotImplementedException();
        }
    }
}
