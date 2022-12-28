using KanbanBAL.Results;
using KanbanDAL;
using KanbanDAL.Entities;
using KanbanDAL.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanBAL.CQRS.Commands.Comments
{
    public class DeleteCommentCommand : IRequest<Result>
    {
        public Guid Id { get; set; }

        public DeleteCommentCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Result>
    {
        private readonly KanbanDbContext _context;
        private readonly ILogger<DeleteCommentCommandHandler> _logger;

        public DeleteCommentCommandHandler(KanbanDbContext context, ILogger<DeleteCommentCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(DeleteCommentCommandHandler)}' has been created.");
        }

        public async Task<Result> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (comment == null)
            {
                _logger.LogError($"Can not find comment with id: {request.Id}");
                return Result.NotFound(request.Id);
            }

            var errors = new List<string>();
            try
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                _logger.LogError(string.Join(Environment.NewLine, errors));
                return Result.BadRequest<ResponseBoardModel>(errors);
            }

            return Result.Ok();
        }
    }
}
