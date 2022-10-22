using KanbanBAL.Results;
using MediatR;

namespace KanbanBAL.CQRS.Commands.Users
{
    public class RegisterUserCommand : IRequest<Result<string>>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
