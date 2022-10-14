using KanbanBAL.Results;
using KanbanDAL;
using KanbanDAL.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KanbanBAL.CQRS.Commands.Boards
{
    public class DeleteBoardCommand : IRequest<Result>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }

        public DeleteBoardCommand(Guid id, string userId)
        {
            Id = id;
            UserId = userId;
        }
    }

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
                _logger.LogError($"Can not find board with id: {request.Id}");
                return Result.Forbidden("You have not permission to delete this board");
            }

            try
            {
                _context.Boards.Remove(board);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>();
                errors.Add(ex.Message);
                _logger.LogError(string.Join(Environment.NewLine, errors));
                return Result.BadRequest<ResponseBoardModel>(errors);
            }

            return Result.Ok();
        }
    }
}
