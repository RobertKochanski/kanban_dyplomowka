using KanbanBAL.Results;
using KanbanDAL;
using KanbanDAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KanbanBAL.CQRS.Commands.Columns
{
    public class CreateColumnCommandHandler : IRequestHandler<CreateColumnCommand, Result>
    {
        private readonly KanbanDbContext _context;
        private readonly ILogger<CreateColumnCommandHandler> _logger;

        public CreateColumnCommandHandler(KanbanDbContext context, ILogger<CreateColumnCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(CreateColumnCommandHandler)}' has been created.");
        }

        public async Task<Result> Handle(CreateColumnCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                _logger.LogError($"[{DateTime.UtcNow}] Complete the field");
                return Result.BadRequest($"Complete the field");
            }

            var board = await _context.Boards.FirstOrDefaultAsync(x => x.Id == request.BoardId);

            if (board == null)
            {
                _logger.LogError($"[{DateTime.UtcNow}] Something goes wrong with this board");
                return Result.BadRequest($"Something goes wrong with this board");
            }

            var column = new Column()
            {
                BoardId = request.BoardId,
                Name = request.Name,
                Jobs = new List<Job>()
            };

            var errors = new List<string>();

            try
            {
                await _context.Columns.AddAsync(column);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                _logger.LogError(string.Join(Environment.NewLine, errors));
                return Result.BadRequest(errors);
            }
            
            return Result.Ok();
        }
    }
}
