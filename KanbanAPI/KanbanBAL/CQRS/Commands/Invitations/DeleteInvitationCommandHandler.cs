using KanbanBAL.Results;
using KanbanDAL;
using KanbanDAL.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KanbanBAL.CQRS.Commands.Invitations
{
    public class DeleteInvitationCommandHandler : IRequestHandler<DeleteInvitationCommand, Result>
    {
        private readonly KanbanDbContext _context;
        private readonly ILogger<DeleteInvitationCommandHandler> _logger;

        public DeleteInvitationCommandHandler(KanbanDbContext context, ILogger<DeleteInvitationCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(DeleteInvitationCommandHandler)}' has been created.");
        }

        public async Task<Result> Handle(DeleteInvitationCommand request, CancellationToken cancellationToken)
        {
            var inv = await _context.Invitations.FirstOrDefaultAsync(x => x.Id == request.InvitationId, cancellationToken);

            if (inv == null)
            {
                _logger.LogError($"Can not find column with id: {request.InvitationId}");
                return Result.NotFound(request.InvitationId);
            }

            var errors = new List<string>();
            try
            {
                _context.Invitations.Remove(inv);
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
