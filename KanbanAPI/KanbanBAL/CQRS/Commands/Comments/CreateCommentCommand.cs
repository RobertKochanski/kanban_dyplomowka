using KanbanBAL.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace KanbanBAL.CQRS.Commands.Comments
{
    public class CreateCommentCommand : IRequest<Result>
    {
        public string Text { get; set; }
        [JsonIgnore]
        public string? Creator { get; set; }
        [JsonIgnore]
        public Guid JobId { get; set; }
    }
}
