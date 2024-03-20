using InventoryTracker.BusinessLogic.Interfaces;
using InventoryTracker.DataAccess.Interfaces;
using InventoryTracker.Domain;

namespace InventoryTracker.BusinessLogic
{
    public class BorrowLogic : IBorrowLogic
    {
        IBorrowRepository repository;

        public BorrowLogic(IBorrowRepository repository)
        {
            this.repository = repository;
        }

        public async Task CreateBorrow(Borrow borrow)
        {
            await repository.CreateBorrow(borrow);
        }

        public async Task<List<Borrow>> GetBorrowsByEmployee(Guid id, bool active)
        {
            return await repository.GetBorrowsByEmployee(id, active);
        }

        public async Task ReturnBorrow(Borrow borrow)
        {
            await repository.ReturnBorrow(borrow);
        }
    }
}
