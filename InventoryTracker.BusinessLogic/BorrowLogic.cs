using InventoryTracker.BusinessLogic.Interfaces;
using InventoryTracker.DataAccess.Enums;
using InventoryTracker.DataAccess.Interfaces;
using InventoryTracker.Domain;
using InventoryTrackerDTO.Borrow;

namespace InventoryTracker.BusinessLogic
{
    public class BorrowLogic : IBorrowLogic
    {
        IBorrowRepository borrowRepo;
        IEmployeeRepository employeeRepo;
        IEquipmentRepository equipmentRepo;

        public BorrowLogic(IBorrowRepository borrowRepository, IEmployeeRepository employeeRepository,IEquipmentRepository equipmentRepository)
        {
            this.borrowRepo = borrowRepository;
            this.employeeRepo = employeeRepository;
            this.equipmentRepo=equipmentRepository;
        }

        public async Task CreateBorrow(CreateBorrowDTO borrowDTO)
        {
            var temp=await FillBorrow(borrowDTO);
            await borrowRepo.CreateBorrow(temp);
        }

        private async Task<Borrow> FillBorrow(CreateBorrowDTO borrowDTO)
        {
            var output=new Borrow();
            output.ClassRoomId=borrowDTO.ClassRoomId;
            output.Equipment = await equipmentRepo.GetEquipmentByInventoryMark(borrowDTO.EquipmentInventoryMark);
            output.EquipmentId = output.Equipment.EquipmentId;
            output.Employee = await employeeRepo.GetEmployeeByMailAddress(borrowDTO.EmployeeMailAddress);
            output.EmployeeId = output.Employee.EmployeeId;
            return output;
        }

        public async Task<List<Borrow>> GetBorrowsByClassRoom(Guid id, bool active)
        {
            return await borrowRepo.GetBorrowsByFilter(id, BorrowSearch.ClassRoomId, active);
        }

        public async Task<List<Borrow>> GetBorrowsByEmployee(string email, bool active)
        {
            var temp = await employeeRepo.GetEmployeeByMailAddress(email);
            return await borrowRepo.GetBorrowsByFilter(temp.EmployeeId,BorrowSearch.EmployeeId, active);
        }

        public async Task<List<Borrow>> GetBorrowsByEquipment(Guid id, bool active)
        {
            return await borrowRepo.GetBorrowsByFilter(id, BorrowSearch.EquipmentId, active);
        }

        public async Task ReturnBorrow(ReturnBorrowDTO borrow)
        {
            var temp = new Borrow();
            temp.EquipmentId=borrow.EquipmentId;
            temp.Employee = await employeeRepo.GetEmployeeByMailAddress(borrow.EmployeeMailAdress);
            temp.EmployeeId=temp.Employee.EmployeeId;
            await borrowRepo.ReturnBorrow(temp);
        }
    }
}
