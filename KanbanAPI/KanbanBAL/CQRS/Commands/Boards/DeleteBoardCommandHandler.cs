using KanbanBAL.Results;
using KanbanDAL;
using KanbanDAL.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KanbanBAL.CQRS.Commands.Boards
{
    public class DeleteBoardCommandHandler : IRequestHandler<DeleteBoardCommand, Result>
    {
        private readonly KanbanDbContext _context;
        private readonly ILogger<DeleteBoardCommandHandler> _logger;

        public DeleteBoardCommandHandler(KanbanDbContext context, ILogger<DeleteBoardCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(DeleteBoardCommandHandler)}' has been created.");
        }

        public async Task<Result> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
        {
            var board = await _context.Boards.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (board == null)
            {
                _logger.LogError($"Can not find board with id: {request.Id}");
                return Result.NotFound(request.Id);
            }

            if (board.OwnerId != request.UserId)
            {
                _logger.LogError("You have not permission to delete this board");
                return Result.Forbidden("You have not permission to delete this board");
            }

            var errors = new List<string>();
            try
            {
                _context.Boards.Remove(board);
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
