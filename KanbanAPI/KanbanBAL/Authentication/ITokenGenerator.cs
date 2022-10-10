using KanbanDAL.Entities;

namespace KanbanBAL.Authentication
{
    public interface ITokenGenerator
    {
        string CreateToken(User user);
    }
}
