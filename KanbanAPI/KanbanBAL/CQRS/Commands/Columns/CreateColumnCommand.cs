using KanbanBAL.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace KanbanBAL.CQRS.Commands.Columns
{
    public class CreateColumnCommand : IRequest<Result>
    {
        [JsonIgnore]
        public Guid BoardId { get; set; }
        public string Name { get; set; }
    }
}
