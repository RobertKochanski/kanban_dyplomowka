using KanbanBAL.Results;
using KanbanDAL;
using KanbanDAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KanbanBAL.CQRS.Commands.Jobs
{
    public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, Result>
    {
        private readonly KanbanDbContext _context;
        private readonly ILogger<UpdateJobCommandHandler> _logger;

        public UpdateJobCommandHandler(KanbanDbContext context, ILogger<UpdateJobCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(UpdateJobCommandHandler)}' has been created.");
        }

        public async Task<Result> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                _logger.LogError($"[{DateTime.UtcNow}] Name field can not be null");
                return Result.BadRequest($"Name field can not be null");
            }

            var job = await _context.Jobs.Include(x => x.Users).FirstOrDefaultAsync(x => x.Id == request.Id);

            request.UserEmails = request.UserEmails.Distinct().ToList();
            List<User> users = new List<User>();

            foreach (var userEmail in request.UserEmails)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userEmail);
                if (user != null)
                {
                    users.Add(user);
                }
            }

            job.Name = request.Name;
            job.Description = request.Description;
            job.Users = users;


            var errors = new List<string>();
            try
            {
                _context.Jobs.Update(job);
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
