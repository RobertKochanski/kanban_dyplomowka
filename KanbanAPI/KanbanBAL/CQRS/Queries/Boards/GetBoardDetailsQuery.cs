using KanbanBAL.Results;
using KanbanDAL.Models;
using MediatR;

namespace KanbanBAL.CQRS.Queries.Boards
{
    public class GetBoardDetailsQuery : IRequest<Result<ResponseBoardModel>>
    {
        public Guid Id { get; set; }

        public GetBoardDetailsQuery(Guid id)
        {
            Id = id;
        }
    }
}
