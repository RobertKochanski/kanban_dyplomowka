using KanbanDAL.Entities;

namespace KanbanDAL.Models
{
    public class ResponseBoardModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ResponseColumnModel>? Columns { get; set; }

        public string OwnerEmail { get; set; }
        public IEnumerable<ResponseUserModel> Members { get; set; }
    }
}
