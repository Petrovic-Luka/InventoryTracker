using InventoryTracker.Domain;
using InventoryTrackerDTO.Borrow;
using InventoryTrackerDTO.Equipment;
using InventoryTrackerDTO.EquipmentType;

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

        public static DisplayBorrowDTO ToHistoryDisplayDTO(this Borrow borrow)
        {
            var temp = new DisplayBorrowDTO();
            temp.EmployeeId = borrow.EmployeeId;
            temp.EquipmentId = borrow.EquipmentId;
            temp.DisplayString = $"{borrow.Equipment?.InventoryMark} {borrow.Employee?.MailAddress} {borrow.StartDate.ToShortDateString()}";
            if(borrow.EndDate != null)
            {
                temp.DisplayString += $" {borrow.EndDate.Value.ToShortDateString()}";
            }
            else
            {
                temp.DisplayString += " /";
            }

            return temp;
        }

        public static EquipmentTypeDTO ToEquipmentTypeDTO(this EquipmentType equipmentType)
        {
            return new EquipmentTypeDTO
            {
                EquipmentTypeId = equipmentType.EquipmentTypeId,
                Name = equipmentType.Name
            };
        }

        public static DisplayEquipmentDTO ToDisplayEquipmentDTO(this Equipment equipment)
        {
            var temp=new DisplayEquipmentDTO();
            temp.EquipmentId= equipment.EquipmentId;
            temp.Description = equipment.Description;
            temp.InventoryMark = equipment.InventoryMark;
            temp.SerialMark=equipment.SerialMark;
            temp.DisplayString = $"{equipment.Description} {equipment.InventoryMark}";
            return temp;
        }
    }
}
