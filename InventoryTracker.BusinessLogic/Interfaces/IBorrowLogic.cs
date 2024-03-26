using InventoryTracker.Domain;

namespace InventoryTracker.BusinessLogic.Interfaces
{
    public interface IBorrowLogic
    {
        public Task CreateBorrow(Borrow borrow);
        public Task ReturnBorrow(Borrow borrow);

        public Task <List<Borrow>> GetBorrowsByEmployee(Guid id,bool active);

        public Task<List<Borrow>> GetBorrowsByClassRoom(Guid id, bool active);
        public Task<List<Borrow>> GetBorrowsByEquipment(Guid id, bool active);

    }
}
