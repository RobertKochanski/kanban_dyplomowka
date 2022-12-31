using KanbanBAL.Results;
using KanbanDAL;
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
    public class RemoveUserFromBoardCommand : IRequest<Result>
    {
        [JsonIgnore]
        public Guid? BoardId { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; }
    }

    public class RemoveUserFromBoardCommandHandler : IRequestHandler<RemoveUserFromBoardCommand, Result>
    {
        private readonly KanbanDbContext _context;
        private readonly ILogger<RemoveUserFromBoardCommandHandler> _logger;

        public RemoveUserFromBoardCommandHandler(KanbanDbContext context, ILogger<RemoveUserFromBoardCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(RemoveUserFromBoardCommandHandler)}' has been created.");
        }

        public async Task<Result> Handle(RemoveUserFromBoardCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
            var board = await _context.Boards.Include(x => x.Members).FirstOrDefaultAsync(x => x.Id == request.BoardId, cancellationToken);

            if (user == null)
            {
                _logger.LogError($"[{DateTime.UtcNow}] Can not find user");
                return Result.BadRequest($"Can not find user");
            }

            if (user == null)
            {
                _logger.LogError($"[{DateTime.UtcNow}] Can not find board");
                return Result.BadRequest($"Can not find board");
            }

            board.Members.Remove(user);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
