namespace InventoryTracker.Domain
{
    public class ClassRoom
    {
        public Guid ClassRoomId { get; set; }

        public string Name { get; set; }

        public List<Borrow> BorrowedItems { get; set; }
    }
}
