using KanbanBAL.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace KanbanBAL.CQRS.Commands.Jobs
{
    public class UpdateJobCommand : IRequest<Result>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<string>? UserEmails { get; set; }
    }
}
