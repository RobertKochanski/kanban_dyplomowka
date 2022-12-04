using KanbanBAL.Results;
using KanbanDAL;
using KanbanDAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KanbanBAL.CQRS.Commands.Invitations
{
    public class CreateInvitationCommandHandler : IRequestHandler<CreateInvitationCommand, Result>
    {
        private readonly KanbanDbContext _context;
        private readonly ILogger<CreateInvitationCommandHandler> _logger;

        public CreateInvitationCommandHandler(KanbanDbContext context, ILogger<CreateInvitationCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(CreateInvitationCommandHandler)}' has been created.");
        }

        public async Task<Result> Handle(CreateInvitationCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.UserEmail, cancellationToken);
            var board = await _context.Boards.Include(x => x.Members).FirstOrDefaultAsync(x => x.Id == request.BoardId, cancellationToken);
            var invitation = await _context.Invitations.Where(x => x.BoardId == request.BoardId && x.UserEmail == request.UserEmail).FirstOrDefaultAsync();

            var errors = new List<string>();

            if (invitation != null)
            {
                _logger.LogError($"[{DateTime.UtcNow}] Invitation already exist");
                return Result.BadRequest("Invitation already exist");
            }

            if (string.IsNullOrEmpty(request.UserEmail))
            {
                _logger.LogError($"[{DateTime.UtcNow}] Email can not be empty");
                errors.Add("Email can not be empty");
            }

            if (board == null)
            {
                _logger.LogError($"[{DateTime.UtcNow}] Board with this Id does not exist");
                errors.Add("Board with this Id does not exist");
            }

            if (user == null)
            {
                _logger.LogError($"[{DateTime.UtcNow}] User with this email does not exist");
                errors.Add("User with this email does not exist");
            }
            else if (board.Members.Contains(user))
            {
                _logger.LogError($"[{DateTime.UtcNow}] User already is member");
                errors.Add("User already is member");
            }

            if (errors.Count > 0)
            {
                return Result.BadRequest(errors);
            }

            var inv = new Invitation()
            {
                Id = request.Id,
                BoardId = request.BoardId,
                UserEmail = request.UserEmail,
                InvitingEmail = request.InvitingEmail,
                InvitedAt = DateTime.Now,
            };

            try
            {
                await _context.AddAsync(inv);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                _logger.LogError(string.Join(Environment.NewLine, errors));
                return Result.BadRequest<Board>(errors);
            }

            return Result.Ok();
        }
    }
}
