namespace KanbanDAL.Entities
{
    public class Board
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Column>? Columns { get; set; }

        public string OwnerEmail { get; set; }
        public List<User> Members { get; set; }
    }
}
