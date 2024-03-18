using InventoryTracker.Domain.Enums;

namespace InventoryTracker.Domain
{
    public class Equipment
    {
        public Guid EquipmentId { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public string SerialMark { get; set; }
        public string InventoryMark { get; set; }
        public EquipmentStatus EquipmentStatus { get; set; }
        public int EquipmentTypeId { get; set; }
        public EquipmentType? EquipmentType { get; set; }

    }
}
