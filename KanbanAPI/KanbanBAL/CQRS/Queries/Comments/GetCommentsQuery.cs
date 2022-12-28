using KanbanBAL.Results;
using KanbanDAL.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace KanbanBAL.CQRS.Queries.Comments
{
    public class GetCommentsQuery : IRequest<Result<List<Comment>>>
    {
        [JsonIgnore]
        public Guid JobId { get; set; }

        public GetCommentsQuery(Guid jobId)
        {
            JobId = jobId;
        }
    }
}
