namespace InventoryTracker.Domain
{
    public class Equipment
    {
        public Guid EquipmentId { get; set; }

        public string Name { get; set; }

        public string SerialMark { get; set; }

        public string InventoryMark { get; set; }

        public int EquipmentTypeId { get; set; }

        public EquipmentType? EquipmentType { get; set; }

    }
}
