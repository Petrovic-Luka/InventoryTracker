using InventoryTracker.Domain;
using InventoryTrackerDTO.Borrow;
using InventoryTrackerDTO.EquipmentType;

namespace InventoryTracker.API.Mappers
{
    public static class ExtensionMethods
    {
        public static DisplayBorrowDTO ToDisplayDTO(this Borrow borrow)
        {
            return new DisplayBorrowDTO
            {
                EmployeeId = borrow.EmployeeId,
                EquipmentId = borrow.EquipmentId,
                DisplayString = $"{borrow.Equipment?.Description} {borrow.Equipment?.InventoryMark}"
            };
        }

        public static EquipmentTypeDTO ToEquipmentTypeDTO(this EquipmentType equipmentType)
        {
            return new EquipmentTypeDTO
            {
                EquipmentTypeId = equipmentType.EquipmentTypeId,
                Name = equipmentType.Name
            };
        }
    }
}
