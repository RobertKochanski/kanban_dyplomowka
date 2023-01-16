using KanbanBAL.Results;
using KanbanDAL;
using KanbanDAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KanbanBAL.CQRS.Commands.Jobs
{
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Result>
    {
        private readonly KanbanDbContext _context;
        private readonly ILogger<CreateJobCommandHandler> _logger;

        public CreateJobCommandHandler(KanbanDbContext context, ILogger<CreateJobCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(CreateJobCommandHandler)}' has been created.");
        }

        public async Task<Result> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                _logger.LogError($"[{DateTime.UtcNow}] Name field can not be null");
                return Result.BadRequest($"Name field can not be null");
            }

            var job = new Job()
            {
                ColumnId = request.ColumnId,
                Name = request.Name,
                Description = request.Description,
                Users = new List<User>(),
                Priority = request.Priority,
                Deadline = request.Deadline,
            };

            request.UserEmails = request.UserEmails.Distinct().ToList();

            foreach (var userEmail in request.UserEmails)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userEmail);
                if (user != null)
                {
                    job.Users.Add(user);
                }
            }

            var errors = new List<string>();

            try
            {
                await _context.Jobs.AddAsync(job);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                _logger.LogError(string.Join(Environment.NewLine, errors));
                return Result.BadRequest<Job>(errors);
            }

            return Result.Ok();
        }
    }
}
