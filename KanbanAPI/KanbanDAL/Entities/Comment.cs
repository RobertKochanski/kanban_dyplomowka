namespace KanbanDAL.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Creator { get; set; }
        public DateTime CreateAt { get; set; }
        public Guid JobId { get; set; }
        public Job Job { get; set; }
    }
}
