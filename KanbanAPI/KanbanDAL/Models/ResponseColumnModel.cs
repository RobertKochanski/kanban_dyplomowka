namespace KanbanDAL.Models
{
    public class ResponseColumnModel
    {
        public Guid Id { get; set; }
        public Guid BoardId { get; set; }
        public string Name { get; set; }
        public IEnumerable<ResponseJobModel> Jobs { get; set; }
    }
}
