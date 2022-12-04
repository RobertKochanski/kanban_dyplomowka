using KanbanBAL.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace KanbanBAL.CQRS.Commands.Invitations
{
    public class CreateInvitationCommand : IRequest<Result>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore]
        public Guid BoardId { get; set; }
        [JsonIgnore]
        public string? InvitingEmail { get; set; }
        public string UserEmail { get; set; }
    }
}
