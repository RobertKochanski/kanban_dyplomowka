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
        private readonly ILogger<GetBoardDetailsQueryHandler> _logger;

        public GetBoardDetailsQueryHandler(KanbanDbContext context, ILogger<GetBoardDetailsQueryHandler> logger)
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
                        }).OrderBy(y => y.Name)
                    }),
                    OwnerEmail = x.OwnerEmail,
                    Members = x.Members.Select(y => new ResponseUserModel
                    {
                        Email = y.Email,
                        Username = y.UserName
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
