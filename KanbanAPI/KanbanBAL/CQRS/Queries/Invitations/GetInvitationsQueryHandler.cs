using KanbanBAL.Results;
using KanbanDAL;
using KanbanDAL.Entities;
using KanbanDAL.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KanbanBAL.CQRS.Queries.Invitations
{
    public class GetInvitationsQueryHandler : IRequestHandler<GetInvitationsQuery, Result<List<ReponseInvitationModel>>>
    {
        private readonly KanbanDbContext _context;
        private readonly ILogger<GetInvitationsQueryHandler> _logger;

        public GetInvitationsQueryHandler(KanbanDbContext context, ILogger<GetInvitationsQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(GetInvitationsQueryHandler)}' has been created.");
        }

        public async Task<Result<List<ReponseInvitationModel>>> Handle(GetInvitationsQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);

            var query = _context.Invitations
                .Where(x => x.UserEmail == user.Email)
                .Include(x => x.Board)
                .Select(x => new ReponseInvitationModel()
                {
                    Id = x.Id,
                    InvitingEmail = x.InvitingEmail,
                    UserEmail = x.UserEmail,
                    InvitedAt = x.InvitedAt,
                    Board = new ResponseBoardModel()
                    {
                        Id = x.Board.Id,
                        CreatedAt = x.Board.CreatedAt,
                        Name = x.Board.Name,
                        OwnerEmail = x.Board.OwnerEmail,
                        Columns = x.Board.Columns.Select(y => new ResponseColumnModel()
                        {
                            Id = y.Id,
                            Name = y.Name,
                            BoardId = y.BoardId,
                            Jobs = y.Jobs.Select(z => new ResponseJobModel()
                            {
                                Id = z.Id,
                                Name = z.Name,
                                Description = z.Description,
                                UserEmails = z.Users.Select(x => x.Email)
                            }),
                        }),
                        Members = x.Board.Members.Select(x => new ResponseUserModel()
                        {
                            Id = x.Id,
                            Username = x.UserName,
                            Email = x.Email,
                        }),
                    }
                })
                .AsNoTracking();

            List<ReponseInvitationModel>? invitation = null;
            var errors = new List<string>();
            try
            {
                invitation = await query.ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                _logger.LogError(string.Join(Environment.NewLine, errors));
                return Result.BadRequest<List<ReponseInvitationModel>>(errors);
            }

            return Result.Ok(invitation);
        }
    }
}
