using KanbanBAL.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace KanbanBAL.CQRS.Commands.Invitations
{
    public class AcceptInvitationCommand : IRequest<Result>
    {
        public Guid InvitationId { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; }

        public AcceptInvitationCommand(Guid invitationId, string userEmail)
        {
            InvitationId = invitationId;
            UserId = userEmail;
        }
    }
}
