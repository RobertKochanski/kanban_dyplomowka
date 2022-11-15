using KanbanBAL.Results;
using KanbanDAL.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace KanbanBAL.CQRS.Queries.Invitations
{
    public class GetInvitationsQuery : IRequest<Result<List<Invitation>>>
    {
        [JsonIgnore]
        public string UserId { get; set; }

        public GetInvitationsQuery(string userId)
        {
            UserId = userId;
        }
    }
}
