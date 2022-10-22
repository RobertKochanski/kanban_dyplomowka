using KanbanBAL.Results;
using KanbanDAL;
using KanbanDAL.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KanbanBAL.CQRS.Commands.Columns
{
    public class DeleteColumnCommandHandler : IRequestHandler<DeleteColumnCommand, Result>
    {
        private readonly KanbanDbContext _context;
        private readonly ILogger<DeleteColumnCommandHandler> _logger;

        public DeleteColumnCommandHandler(KanbanDbContext context, ILogger<DeleteColumnCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(DeleteColumnCommandHandler)}' has been created.");
        }

        public async Task<Result> Handle(DeleteColumnCommand request, CancellationToken cancellationToken)
        {
            var column = await _context.Columns.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (column == null)
            {
                _logger.LogError($"Can not find column with id: {request.Id}");
                return Result.NotFound(request.Id);
            }

            var errors = new List<string>();
            try
            {
                _context.Columns.Remove(column);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                _logger.LogError(string.Join(Environment.NewLine, errors));
                return Result.BadRequest<ResponseBoardModel>(errors);
            }

            return Result.Ok();
        }
    }
}
