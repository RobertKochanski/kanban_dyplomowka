using KanbanBAL.CQRS.Commands.Boards;
using KanbanBAL.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KanbanBAL.CQRS.Commands.Jobs
{
    public class CreateJobCommand : IRequest<Result>
    {
        [JsonIgnore]
        public Guid ColumnId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<string>? UserEmails { get; set; }
    }
}
