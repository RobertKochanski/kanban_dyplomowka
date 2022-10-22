using KanbanBAL.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace KanbanBAL.CQRS.Commands.Boards
{
    public class DeleteBoardCommand : IRequest<Result>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; }

        public DeleteBoardCommand(Guid id, string userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}
