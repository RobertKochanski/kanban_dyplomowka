namespace KanbanDAL.Entities
{
    public class Invitation
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public string InvitingEmail { get; set; }
        public DateTime InvitedAt { get; set; }
        public Guid BoardId { get; set; }
        public Board Board { get; set; }
    }
}
