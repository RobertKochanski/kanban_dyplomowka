using KanbanBAL.Results;
using KanbanDAL;
using KanbanDAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KanbanBAL.CQRS.Queries.Invitations
{
    public class GetInvitationsQueryHandler : IRequestHandler<GetInvitationsQuery, Result<List<Invitation>>>
    {
        private readonly KanbanDbContext _context;
        private readonly ILogger<GetInvitationsQueryHandler> _logger;

        public GetInvitationsQueryHandler(KanbanDbContext context, ILogger<GetInvitationsQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(GetInvitationsQueryHandler)}' has been created.");
        }

        public async Task<Result<List<Invitation>>> Handle(GetInvitationsQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);

            var query = _context.Invitations
                .Where(x => x.UserEmail == user.Email)
                .AsNoTracking();

            List<Invitation>? invitation = null;
            var errors = new List<string>();
            try
            {
                invitation = await query.ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                _logger.LogError(string.Join(Environment.NewLine, errors));
                return Result.BadRequest<List<Invitation>>(errors);
            }

            return Result.Ok(invitation);
        }
    }
}
