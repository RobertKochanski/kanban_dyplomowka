using KanbanBAL.Results;
using KanbanDAL;
using KanbanDAL.Entities;
using KanbanDAL.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace KanbanBAL.CQRS.Commands.Columns
{
    public class MovingJobsCommand : IRequest<Result>
    {
        public MovingJobsCommandObject obj { get; set; }
    }

    public class MovingJobsCommandObject
    {
        public Guid currentColumnId { get; set; }
        public List<ResponseJobModel>? currentContainer { get; set; }
    }

    public class MovingJobsCommandHandler : IRequestHandler<MovingJobsCommand, Result>
    {
        private readonly KanbanDbContext _context;
        private readonly ILogger<MovingJobsCommandHandler> _logger;

        public MovingJobsCommandHandler(KanbanDbContext context, ILogger<MovingJobsCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(MovingJobsCommandHandler)}' has been created.");
        }
        public async Task<Result> Handle(MovingJobsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<Job> jobs = new List<Job>();
                foreach (var job in request.obj.currentContainer)
                {
                    var task = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == job.Id);
                    task.ColumnId = request.obj.currentColumnId;

                    jobs.Add(task);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>();
                errors.Add(ex.Message);
                _logger.LogError(string.Join(Environment.NewLine, errors));
                return Result.BadRequest(errors);
            }

            return Result.Ok();
        }
    }
}
