namespace InventoryTrackerDTO.Equipment
{
    public class CreateEquipmentDTO
    {
        public string Description { get; set; }
        public string? Note { get; set; }
        public string SerialMark { get; set; }
        public string InventoryMark { get; set; }
        public int EquipmentTypeId { get; set; }
    }
}
