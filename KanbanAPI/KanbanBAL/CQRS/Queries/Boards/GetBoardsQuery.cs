using KanbanBAL.Results;
using KanbanDAL.Models;
using MediatR;

namespace KanbanBAL.CQRS.Queries.Boards
{
    public class GetBoardsQuery : IRequest<Result<List<ResponseBoardModel>>>
    {
    }
}
