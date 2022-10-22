using KanbanBAL.Results;
using KanbanDAL;
using KanbanDAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KanbanBAL.CQRS.Queries.Jobs
{
    public class GetJobDetailsQueryHandler : IRequestHandler<GetJobDetailsQuery, Result<Job>>
    {
        private readonly KanbanDbContext _context;
        private readonly ILogger<GetJobDetailsQueryHandler> _logger;

        public GetJobDetailsQueryHandler(KanbanDbContext context, ILogger<GetJobDetailsQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(GetJobDetailsQueryHandler)}' has been created.");
        }

        public async Task<Result<Job>> Handle(GetJobDetailsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Jobs
                .AsNoTracking();

            Job? job = new Job();
            var errors = new List<string>();

            try
            {
                job = await query.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            }
            catch (Exception e)
            {
                errors.Add(e.Message);
                _logger.LogError(string.Join(Environment.NewLine, errors));
                return Result.BadRequest<Job>(errors);
            }

            if (job == null)
            {
                return Result.NotFound<Job>(request.Id);
            }

            return Result.Ok(job);
        }
    }
}
