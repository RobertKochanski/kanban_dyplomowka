using KanbanBAL.Results;
using KanbanDAL.Models;
using MediatR;

namespace KanbanBAL.CQRS.Commands.Users
{
    public class LoginUserCommand : IRequest<Result<ResponseUserModel>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
