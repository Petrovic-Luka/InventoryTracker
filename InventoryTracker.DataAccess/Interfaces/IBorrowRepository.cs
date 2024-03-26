using InventoryTracker.DataAccess.Enums;
using InventoryTracker.Domain;

namespace InventoryTracker.DataAccess.Interfaces
{
    public interface IBorrowRepository
    {
        public Task CreateBorrow(Borrow borrow);

        public Task ReturnBorrow(Borrow borrow);

        public Task<List<Borrow>> GetBorrowsByFilter(Guid id,BorrowSearch criteria, bool active);
    }
}
