namespace InventoryTrackerDTO.Borrow
{
    public class CreateBorrowDTO
    {
        public string EmployeeMailAddress { get; set; }
        public string EquipmentInventoryMark { get; set; }
        public Guid ClassRoomId { get; set; }


    }
}
