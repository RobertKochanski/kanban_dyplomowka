using KanbanBAL.Results;
using KanbanDAL;
using KanbanDAL.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KanbanBAL.CQRS.Queries.Boards
{
    public class GetBoardDetailsQueryHandler : IRequestHandler<GetBoardDetailsQuery, Result<ResponseBoardModel>>
    {
        private readonly KanbanDbContext _context;
        private readonly ILogger<GetBoardsQueryHandler> _logger;

        public GetBoardDetailsQueryHandler(KanbanDbContext context, ILogger<GetBoardsQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(GetBoardDetailsQueryHandler)}' has been created.");
        }

        public async Task<Result<ResponseBoardModel>> Handle(GetBoardDetailsQuery request, CancellationToken cancellationToken)
        {
            var boardQuery = _context.Boards
                .Include(x => x.Columns)
                    .ThenInclude(x => x.Jobs)
                .Include(x => x.Members)
                .Select(x => new ResponseBoardModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Columns = x.Columns,
                    OwnerId = x.OwnerId,
                    Members = x.Members.Select(y => new ResponseMemberModel
                    {
                        Email = y.Email,
                        UserName = y.UserName
                    })
                })
                .AsNoTracking();

            var errors = new List<string>();
            ResponseBoardModel? board = null;
            try
            {
                board = await boardQuery
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            }
            catch (Exception e)
            {
                errors.Add(e.Message);
                _logger.LogError(string.Join(Environment.NewLine, errors));
                return Result.BadRequest<ResponseBoardModel>(errors);
            }

            if (board == null)
            {
                return Result.NotFound<ResponseBoardModel>(request.Id);
            }            

            return Result.Ok(board);
        }
    }
}
