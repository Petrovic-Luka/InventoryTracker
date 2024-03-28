namespace InventoryTrackerDTO.Equipment
{
    public class DisplayEquipmentDTO
    {
        public Guid EquipmentId { get; set; }
        public string Description { get; set; }
        public string SerialMark { get; set; }
        public string InventoryMark { get; set; }
        public string DisplayString { get; set; }
    }
}
