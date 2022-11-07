using KanbanBAL.Results;
using KanbanDAL.Models;
using MediatR;

namespace KanbanBAL.CQRS.Queries.Jobs
{
    public class GetJobDetailsQuery : IRequest<Result<ResponseJobModel>>
    {
        public Guid Id { get; set; }

        public GetJobDetailsQuery(Guid id)
        {
            Id = id;
        }
    }
}
