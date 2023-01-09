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
        private readonly ILogger<GetUserBoardsQueryHandler> _logger;

        public GetUserBoardsQueryHandler(KanbanDbContext context, ILogger<GetUserBoardsQueryHandler> logger)
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
                        .ThenInclude(x => x.Users)
                .Include(x => x.Members)
                .Where(x => x.Members.Contains(user))
                .Select(x => new ResponseBoardModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedAt = x.CreatedAt,
                    Columns = x.Columns.Select(y => new ResponseColumnModel
                    {
                        Id = y.Id,
                        BoardId = y.BoardId,
                        Name = y.Name,
                        Jobs = y.Jobs.Select(z => new ResponseJobModel
                        {
                            Id = z.Id,
                            Name = z.Name,
                            Description = z.Description,
                            UserEmails = z.Users.Select(em => em.Email),
                            ColumnId = z.ColumnId
                        })
                    }),
                    OwnerEmail = x.OwnerEmail,
                    Members = x.Members.Select(y => new ResponseUserModel
                    {
                        Id = y.Id,
                        Email = y.Email,
                        Username = y.UserName
                    })
                })
                .OrderBy(x => x.Name)
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
