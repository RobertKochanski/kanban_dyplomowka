using KanbanBAL.Results;
using KanbanDAL;
using KanbanDAL.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KanbanBAL.CQRS.Queries.Boards
{
    public class GetUserBoardsQueryHandler : IRequestHandler<GetUserBoardsQuery, Result<List<ResponseBoardModel>>>
    {
        private readonly KanbanDbContext _context;
        private readonly ILogger<GetBoardsQueryHandler> _logger;

        public GetUserBoardsQueryHandler(KanbanDbContext context, ILogger<GetBoardsQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(GetUserBoardsQueryHandler)}' has been created.");
        }

        public async Task<Result<List<ResponseBoardModel>>> Handle(GetUserBoardsQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);

            var boardsQuery = _context.Boards
                .Include(x => x.Columns)
                    .ThenInclude(x => x.Jobs)
                .Include(x => x.Members)
                .Where(x => x.Members.Contains(user))
                .Select(x => new ResponseBoardModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Columns = x.Columns,
                    OwnerId = x.OwnerId,
                    Members = x.Members.Select(y => new ResponseMemberModel
                    {
                        Id = y.Id,
                        Email = y.Email,
                        UserName = y.UserName
                    })
                })
                .AsNoTracking();

            List<ResponseBoardModel>? boards = null;
            var errors = new List<string>();
            try
            {
                boards = await boardsQuery
                    .ToListAsync(cancellationToken);
            }
            catch (Exception e)
            {
                errors.Add(e.Message);
                _logger.LogError(string.Join(Environment.NewLine, errors));
                return Result.BadRequest<List<ResponseBoardModel>>(errors);
            }

            return Result.Ok(boards);
        }
    }
}
