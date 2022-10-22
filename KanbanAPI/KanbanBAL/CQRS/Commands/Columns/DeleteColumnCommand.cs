using KanbanBAL.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace KanbanBAL.CQRS.Commands.Columns
{
    public class DeleteColumnCommand : IRequest<Result>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public DeleteColumnCommand(Guid id)
        {
            Id = id;
        }
    }
}
