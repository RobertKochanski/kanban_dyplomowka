using KanbanBAL.Results;
using KanbanDAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KanbanBAL.CQRS.Commands.Jobs
{
    public class DeleteJobCommand : IRequest<Result>
    {
        public Guid Id { get; set; }

        public DeleteJobCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, Result>
    {
        private readonly KanbanDbContext _context;
        private readonly ILogger<DeleteJobCommandHandler> _logger;

        public DeleteJobCommandHandler(KanbanDbContext context, ILogger<DeleteJobCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(DeleteJobCommandHandler)}' has been created.");
        }

        public async Task<Result> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (job == null)
            {
                _logger.LogError($"Can not find job with id: {request.Id}");
                return Result.NotFound(request.Id);
            }

            var errors = new List<string>();
            try
            {
                _context.Jobs.Remove(job);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                _logger.LogError(string.Join(Environment.NewLine, errors));
                return Result.BadRequest(errors);
            }

            return Result.Ok();
        }
    }
}
