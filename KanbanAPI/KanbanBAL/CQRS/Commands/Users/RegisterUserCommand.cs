using KanbanBAL.Results;
using KanbanDAL.Models;
using MediatR;

namespace KanbanBAL.CQRS.Commands.Users
{
    public class RegisterUserCommand : IRequest<Result<ResponseUserModel>>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
