﻿using KanbanBAL.Results;
using KanbanDAL.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace KanbanBAL.CQRS.Queries.Invitations
{
    public class GetInvitationsQuery : IRequest<Result<List<ReponseInvitationModel>>>
    {
        [JsonIgnore]
        public string UserId { get; set; }

        public GetInvitationsQuery(string userId)
        {
            UserId = userId;
        }
    }
}
