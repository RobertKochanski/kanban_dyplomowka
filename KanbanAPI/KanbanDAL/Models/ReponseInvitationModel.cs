namespace KanbanDAL.Models
{
    public class ReponseInvitationModel
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public string InvitingEmail { get; set; }
        public DateTime InvitedAt { get; set; }
        public ResponseBoardModel Board { get; set; }
    }
}
