using AutoMapper;
using InventoryTracker.Domain;
using InventoryTrackerDTO.Equipment;

namespace InventoryTracker.API.Mappers
{
    public class EquipmentMapper: Profile
    {
        public EquipmentMapper()
        {
            CreateMap<Equipment, CreateEquipmentDTO>().ReverseMap();
        }
    }
}
