using AutoMapper;
using InventoryTracker.Domain;
using InventoryTrackerDTO.Borrow;

namespace InventoryTracker.API.Mappers
{
    public class BorrowMapper: Profile
    {
        public BorrowMapper()
        {
            CreateMap<Borrow, CreateBorrowDTO>().ReverseMap();
            CreateMap<Borrow, ReturnBorrowDTO>().ReverseMap();
        }

    }
}
