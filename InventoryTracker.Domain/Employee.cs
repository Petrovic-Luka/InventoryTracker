namespace InventoryTracker.Domain
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MailAddress { get; set; }

        public List<Borrow>? BorrowedEqupiment { get; set; }

    }
}
