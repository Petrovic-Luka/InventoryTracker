namespace InventoryTracker.Domain
{
    public class EquipmentType
    {
        public Guid EquipmentTypeId { get; set; }

        public string Name { get; set; }

        public List<Equipment>? Equipment { get; set; }
    }
}
