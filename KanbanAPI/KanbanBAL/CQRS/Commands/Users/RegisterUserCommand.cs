using KanbanBAL.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace KanbanBAL.CQRS.Commands.Users
{
    public class RegisterUserCommand : IRequest<Result>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        [JsonIgnore]
        public string? Link { get; set; }
    }
}
