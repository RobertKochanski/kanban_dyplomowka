using KanbanBAL.Results;
using KanbanDAL;
using KanbanDAL.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KanbanBAL.CQRS.Commands.Comments
{
    public class CreateCommantCommandHandler : IRequestHandler<CreateCommentCommand, Result>
    {
        private readonly KanbanDbContext _context;
        private readonly ILogger<CreateCommantCommandHandler> _logger;

        public CreateCommantCommandHandler(KanbanDbContext context, ILogger<CreateCommantCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(CreateCommantCommandHandler)}' has been created.");
        }
        public async Task<Result> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Text))
            {
                _logger.LogError($"[{DateTime.UtcNow}] Complete the field");
                return Result.BadRequest($"Complete the field");
            }

            var comment = new Comment()
            {
                Text = request.Text,
                Creator = request.Creator,
                CreateAt = DateTime.Now,
                JobId = request.JobId,
            };

            var errors = new List<string>();

            try
            {
                await _context.Comments.AddAsync(comment, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation($"[{DateTime.UtcNow}] Board was created.");
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
