using KanbanBAL.Results;
using KanbanDAL.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace KanbanBAL.CQRS.Queries.Boards
{
    public class GetUserBoardsQuery : IRequest<Result<List<ResponseBoardModel>>>
    {
        [JsonIgnore]
        public string UserId { get; set; }

        public GetUserBoardsQuery(string userId)
        {
            UserId = userId;
        }
    }
}
