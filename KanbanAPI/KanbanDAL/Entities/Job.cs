namespace KanbanDAL.Entities
{
    public class Job
    {
        public Guid Id { get; set; }
        public Guid ColumnId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<User>? Users { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
