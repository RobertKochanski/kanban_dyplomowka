namespace KanbanDAL.Entities
{
    public class Invitation
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public Guid BoardId { get; set; }
    }
}
