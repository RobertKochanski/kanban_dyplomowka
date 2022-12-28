using KanbanBAL.Results;
using KanbanDAL;
using KanbanDAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KanbanBAL.CQRS.Queries.Comments
{
    public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, Result<List<Comment>>>
    {
        private readonly KanbanDbContext _context;
        private readonly ILogger<GetCommentsQueryHandler> _logger;

        public GetCommentsQueryHandler(KanbanDbContext context, ILogger<GetCommentsQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(GetCommentsQueryHandler)}' has been created.");
        }

        public async Task<Result<List<Comment>>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            var commentsQuery = _context.Comments.Where(x => x.JobId == request.JobId).AsNoTracking();

            List<Comment>? comments = null;
            var errors = new List<string>();
            try
            {
                comments = await commentsQuery
                    .ToListAsync(cancellationToken);
            }
            catch (Exception e)
            {
                errors.Add(e.Message);
                _logger.LogError(string.Join(Environment.NewLine, errors));
                return Result.BadRequest<List<Comment>>(errors);
            }

            return Result.Ok(comments);
        }
    }
}
