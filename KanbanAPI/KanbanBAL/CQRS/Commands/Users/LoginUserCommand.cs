using KanbanBAL.Results;
using MediatR;

namespace KanbanBAL.CQRS.Commands.Users
{
    public class LoginUserCommand : IRequest<Result<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
