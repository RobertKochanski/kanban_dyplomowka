using KanbanBAL.Results;
using KanbanDAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KanbanBAL.CQRS.Commands.Invitations
{
    public class AcceptInvitationCommandHandler : IRequestHandler<AcceptInvitationCommand, Result>
    {
        private readonly KanbanDbContext _context;
        private readonly ILogger<AcceptInvitationCommandHandler> _logger;

        public AcceptInvitationCommandHandler(KanbanDbContext context, ILogger<AcceptInvitationCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(AcceptInvitationCommandHandler)}' has been created.");
        }

        public async Task<Result> Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            var invitation = await _context.Invitations.FirstOrDefaultAsync(x => x.Id == request.InvitationId);

            if (invitation == null)
            {
                _logger.LogError($"Can not find invitation with id: {request.InvitationId}");
                return Result.NotFound(request.InvitationId);
            }

            if (user == null)
            {
                _logger.LogError($"Can not find user with id: {request.UserId}");
                return Result.NotFound(Guid.Parse(request.UserId));
            }

            var board = await _context.Boards
                .Include(x => x.Members)
                .FirstOrDefaultAsync(x => x.Id == invitation.BoardId);

            if (board == null)
            {
                _logger.LogError($"Can not find board with id: {invitation.BoardId}");
                return Result.NotFound(invitation.BoardId);
            }

            board.Members.Add(user);
            await _context.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
