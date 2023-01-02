using KanbanBAL.Results;
using MediatR;

namespace KanbanBAL.CQRS.Commands.Jobs
{
    public class DeleteJobCommand : IRequest<Result>
    {
        public Guid Id { get; set; }

        public DeleteJobCommand(Guid id)
        {
            Id = id;
        }
    }
}
