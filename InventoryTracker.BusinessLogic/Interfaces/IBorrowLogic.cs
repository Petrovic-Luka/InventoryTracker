using InventoryTracker.Domain;
using InventoryTrackerDTO.Borrow;

namespace InventoryTracker.BusinessLogic.Interfaces
{
    public interface IBorrowLogic
    {
        public Task CreateBorrow(CreateBorrowDTO borrow);
        public Task ReturnBorrow(ReturnBorrowDTO borrow);
        public Task <List<Borrow>> GetBorrowsByEmployee(string email,bool active);
        public Task<List<Borrow>> GetBorrowsByClassRoom(Guid id, bool active);
        public Task<List<Borrow>> GetBorrowsByEquipment(Guid id, bool active);

    }
}
