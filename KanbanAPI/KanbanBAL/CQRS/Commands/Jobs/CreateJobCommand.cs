using KanbanBAL.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace KanbanBAL.CQRS.Commands.Jobs
{
    public class CreateJobCommand : IRequest<Result>
    {
        [JsonIgnore]
        public Guid ColumnId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<string>? UserEmails { get; set; }
        public DateTime? Deadline { get; set; }
        public string? Priority { get; set; }
    }
}
