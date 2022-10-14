using Microsoft.AspNetCore.Identity;

namespace KanbanDAL.Entities
{
    public class User : IdentityUser
    {
        public List<Board> Boards { get; set; }
    }
}
