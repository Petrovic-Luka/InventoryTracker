namespace InventoryTracker.Domain
{
    public class Borrow
    {

        public Guid EquipmentId { get; set; }
        public Guid EmployeeId { get; set; }
        public Equipment? Equipment { get; set; }
        public Employee? Employee { get; set; }
        public int ClassRoomId { get; set; }
        public ClassRoom? ClassRoom { get; set; }
    }
}
