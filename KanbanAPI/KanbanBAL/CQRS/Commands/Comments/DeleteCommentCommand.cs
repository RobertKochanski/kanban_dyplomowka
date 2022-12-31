using KanbanBAL.Results;
using MediatR;

namespace KanbanBAL.CQRS.Commands.Comments
{
    public class DeleteCommentCommand : IRequest<Result>
    {
        public Guid Id { get; set; }

        public DeleteCommentCommand(Guid id)
        {
            Id = id;
        }
    }
}
