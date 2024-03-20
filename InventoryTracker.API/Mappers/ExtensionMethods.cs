using InventoryTracker.Domain;
using InventoryTrackerDTO.Borrow;

namespace InventoryTracker.API.Mappers
{
    public static class ExtensionMethods
    {
        public static DisplayBorrowDTO ToDisplayDTO(this Borrow borrow)
        {
            var temp = new DisplayBorrowDTO();
            temp.EmployeeId = borrow.EmployeeId;
            temp.EquipmentId = borrow.EquipmentId;
            temp.DisplayString = $"{borrow.Equipment?.Description} {borrow.Equipment?.InventoryMark}";
            return temp;
        }

    }
}
