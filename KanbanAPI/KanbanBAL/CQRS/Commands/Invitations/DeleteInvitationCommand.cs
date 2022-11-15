using KanbanBAL.Results;
using MediatR;

namespace KanbanBAL.CQRS.Commands.Invitations
{
    public class DeleteInvitationCommand : IRequest<Result>
    {
        public Guid InvitationId { get; set; }

        public DeleteInvitationCommand(Guid invitationId)
        {
            InvitationId = invitationId;
        }
    }
}
