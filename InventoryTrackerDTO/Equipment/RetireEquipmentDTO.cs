using InventoryTracker.Domain.Enums;

namespace InventoryTrackerDTO.Equipment
{
    public class RetireEquipmentDTO
    {
        public Guid EquipmentId { get; set; }

        public int status { get; set; }
    }
}
