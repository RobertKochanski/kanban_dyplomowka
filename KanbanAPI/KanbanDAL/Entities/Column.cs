namespace KanbanDAL.Entities
{
    public class Column
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid BoardId { get; set; }
        public List<Job> Jobs { get; set; }
    }
}
