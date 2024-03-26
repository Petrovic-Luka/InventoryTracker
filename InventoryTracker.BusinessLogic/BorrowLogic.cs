using InventoryTracker.BusinessLogic.Interfaces;
using InventoryTracker.DataAccess.Enums;
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

        public async Task<List<Borrow>> GetBorrowsByClassRoom(Guid id, bool active)
        {
            return await repository.GetBorrowsByFilter(id, BorrowSearch.ClassRoomId, active);
        }

        public async Task<List<Borrow>> GetBorrowsByEmployee(Guid id, bool active)
        {
            return await repository.GetBorrowsByFilter(id,BorrowSearch.EmployeeId, active);
        }

        public async Task<List<Borrow>> GetBorrowsByEquipment(Guid id, bool active)
        {
            return await repository.GetBorrowsByFilter(id, BorrowSearch.EquipmentId, active);
        }

        public async Task ReturnBorrow(Borrow borrow)
        {
            await repository.ReturnBorrow(borrow);
        }
    }
}
