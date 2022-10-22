using KanbanBAL.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace KanbanBAL.CQRS.Commands.Boards
{
    public class UpdateBoardCommand : IRequest<Result>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; }
        public string Name { get; set; }

        public UpdateBoardCommand(string name, Guid id, string userId)
        {
            Name = name;
            Id = id;
            UserId = userId;
        }
    }
}
