using KanbanBAL.Results;
using KanbanDAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KanbanBAL.CQRS.Commands.Columns
{
    public class UpdateColumnCommandHandler : IRequestHandler<UpdateColumnCommand, Result>
    {
        private readonly KanbanDbContext _context;
        private readonly ILogger<UpdateColumnCommandHandler> _logger;

        public UpdateColumnCommandHandler(KanbanDbContext context, ILogger<UpdateColumnCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(UpdateColumnCommandHandler)}' has been created.");
        }

        public async Task<Result> Handle(UpdateColumnCommand request, CancellationToken cancellationToken)
        {
            var column = await _context.Columns.FirstOrDefaultAsync(x => x.Id == request.ColumnId);

            if (column == null)
            {
                _logger.LogError($"Can not find column with id: {request.ColumnId}");
                return Result.NotFound(request.ColumnId);
            }

            try
            {
                column.Name = request.Name;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>();
                errors.Add(ex.Message);
                _logger.LogError(string.Join(Environment.NewLine, errors));
                return Result.BadRequest(errors);
            }

            return Result.Ok();
        }
    }
}
