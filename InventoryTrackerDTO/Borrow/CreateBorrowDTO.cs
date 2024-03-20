namespace InventoryTrackerDTO.Borrow
{
    public class CreateBorrowDTO
    {
        public Guid EquipmentId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid ClassRoomId { get; set; }


    }
}
