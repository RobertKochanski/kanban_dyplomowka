using KanbanBAL.Results;
using MediatR;

namespace KanbanBAL.CQRS.Commands.Users
{
    public class ConfirmEmailCommand : IRequest<Result>
    {
        public string username { get; set; }

        public ConfirmEmailCommand(string username)
        {
            this.username = username;
        }
    }
}
