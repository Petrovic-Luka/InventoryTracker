using FluentValidation;
using InventoryTracker.BusinessLogic.Interfaces;
using InventoryTracker.DataAccess.Interfaces;
using InventoryTracker.Domain;
using InventoryTracker.Validators;

namespace InventoryTracker.BusinessLogic
{

    public class EquipmentLogic : IEquipmentLogic
    {
        IEquipmentRepository repository;
        IValidator<Equipment> validator;
        public EquipmentLogic(IEquipmentRepository repo, IValidator<Equipment> validator)
        {
            repository = repo;
            this.validator=validator;
        }

        public async Task CreateEquipment(Equipment equipment)
        {
            var result = validator.Validate(equipment);
            if (!result.IsValid)
            {
                var errors = "";
                foreach(var error in result.Errors)
                {
                    errors += $" {error.ErrorMessage}";
                }
                throw new ArgumentException(errors);
            }

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
