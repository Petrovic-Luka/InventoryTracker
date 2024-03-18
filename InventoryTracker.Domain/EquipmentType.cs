namespace InventoryTracker.Domain
{
    public class EquipmentType
    {
        public int EquipmentTypeId { get; set; }

        public string Name { get; set; }

        public List<Equipment>? Equipment { get; set; }
    }
}
