using KanbanBAL.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace KanbanBAL.CQRS.Commands.Columns
{
    public class UpdateColumnCommand : IRequest<Result>
    {
        [JsonIgnore]
        public Guid ColumnId { get; set; }
        public string Name { get; set; }

        public UpdateColumnCommand(Guid columnId, string name)
        {
            ColumnId = columnId;
            Name = name;
        }
    }
}
