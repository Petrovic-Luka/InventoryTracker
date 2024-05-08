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

        public async Task RetireEquipment(Equipment equipment)
        {
            int status = (int)equipment.EquipmentStatus;
            if(status!=2 && status!=3)
            {
                throw new ArgumentException("Equipment status not valid");
            }
            await repository.RetireEquipment(equipment);
        }

        public async Task<Equipment> GetEquipmentById(Guid id)
        {
            return await repository.GetEquipmentById(id);
        }

        public async Task<List<Equipment>> GetEquipmentByType(int typeId, bool available)
        {
            return await repository.GetEquipmentByType(typeId, available);
        }

        public Task UpdateEquipment(Equipment equipment)
        {
            throw new NotImplementedException();
        }

        public async Task<Equipment> GetEquipmentByInventoryMark(string mark)
        {
            if(mark == null || mark.Length!=10)
            {
                throw new ArgumentException("Inventory mark not valid, it must be 10 characters long");
            }
            return await repository.GetEquipmentByInventoryMark(mark);
        }
    }
}
