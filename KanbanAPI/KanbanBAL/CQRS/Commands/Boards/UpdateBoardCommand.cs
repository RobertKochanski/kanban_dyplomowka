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
    public class UpdateBoardCommand : IRequest<Result>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; }
        public string Name { get; set; }

        public UpdateBoardCommand(string name, Guid id, string userId)
        {
            Name = name;
            Id = id;
            UserId = userId;
        }
    }

    public class UpdateBoardCommandHandler : IRequestHandler<UpdateBoardCommand, Result>
    {
        private readonly KanbanDbContext _context;
        private readonly ILogger<UpdateBoardCommandHandler> _logger;

        public UpdateBoardCommandHandler(KanbanDbContext context, ILogger<UpdateBoardCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(UpdateBoardCommandHandler)}' has been created.");
        }

        public async Task<Result> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
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
                board.Name = request.Name;
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
