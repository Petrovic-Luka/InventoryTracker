namespace InventoryTracker.Domain
{
    public class Borrow
    {

        public Guid EquipmentId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public Guid ClassRoomId { get; set; }
        public DateTime? EndDate { get; set; }
        public ClassRoom? ClassRoom { get; set; }
        public Equipment? Equipment { get; set; }
        public Employee? Employee { get; set; }
    }
}
