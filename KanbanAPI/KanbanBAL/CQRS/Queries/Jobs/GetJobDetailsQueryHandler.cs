using KanbanBAL.Results;
using KanbanDAL;
using KanbanDAL.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KanbanBAL.CQRS.Queries.Jobs
{
    public class GetJobDetailsQueryHandler : IRequestHandler<GetJobDetailsQuery, Result<ResponseJobModel>>
    {
        private readonly KanbanDbContext _context;
        private readonly ILogger<GetJobDetailsQueryHandler> _logger;

        public GetJobDetailsQueryHandler(KanbanDbContext context, ILogger<GetJobDetailsQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(GetJobDetailsQueryHandler)}' has been created.");
        }

        public async Task<Result<ResponseJobModel>> Handle(GetJobDetailsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Jobs
                .Include(x => x.Users)
                .Select(x => new ResponseJobModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    UserEmails = x.Users.Select(u => u.Email).ToList()
                })
                .AsNoTracking();

            ResponseJobModel? job = new ResponseJobModel();
            var errors = new List<string>();

            try
            {
                job = await query.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            }
            catch (Exception e)
            {
                errors.Add(e.Message);
                _logger.LogError(string.Join(Environment.NewLine, errors));
                return Result.BadRequest<ResponseJobModel>(errors);
            }

            if (job == null)
            {
                return Result.NotFound<ResponseJobModel>(request.Id);
            }

            return Result.Ok(job);
        }
    }
}
