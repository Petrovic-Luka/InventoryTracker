using FluentValidation;
using InventoryTracker.Domain;

namespace InventoryTracker.Validators
{
    public class EquipmentValidator:AbstractValidator<Equipment>
    {
        public EquipmentValidator()
        {
            RuleFor(x=>x.Description).NotEmpty();
            RuleFor(x=>x.EquipmentTypeId).InclusiveBetween(1,4);
            RuleFor(x => x.InventoryMark).NotEmpty().Length(10);
            RuleFor(x => x.SerialMark).NotEmpty().Length(10);
        }

    }
}
