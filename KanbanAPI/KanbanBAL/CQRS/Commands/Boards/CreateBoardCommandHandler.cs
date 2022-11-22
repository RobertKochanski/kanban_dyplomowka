using KanbanBAL.Results;
using KanbanDAL;
using KanbanDAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KanbanBAL.CQRS.Commands.Boards
{
    public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, Result>
    {
        private readonly KanbanDbContext _context;
        private readonly ILogger<CreateBoardCommandHandler> _logger;

        public CreateBoardCommandHandler(KanbanDbContext context, ILogger<CreateBoardCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(CreateBoardCommandHandler)}' has been created.");
        }

        public async Task<Result> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                _logger.LogError($"[{DateTime.UtcNow}] Complete the field");
                return Result.BadRequest($"Complete the field");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.OwnerEmail, cancellationToken);

            if (user == null)
            {
                _logger.LogError($"[{DateTime.UtcNow}] Something goes wrong");
                return Result.BadRequest($"Something goes wrong");
            }

            if (request.InitialSettings == false)
            {
                var board = new Board()
                {
                    Name = request.Name,
                    CreatedAt = DateTime.Now,
                    OwnerEmail = request.OwnerEmail,
                    Members = new List<User>()
                };

                board.Members.Add(user);

                var errors = new List<string>();

                try
                {
                    await _context.AddAsync(board, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    _logger.LogInformation($"[{DateTime.UtcNow}] Board was created.");
                }
                catch (Exception ex)
                {
                    errors.Add(ex.Message);
                    _logger.LogError(string.Join(Environment.NewLine, errors));
                    return Result.BadRequest<Board>(errors);
                }

                return Result.Ok();
            }
            else
            {
                var board = new Board()
                {
                    Name = request.Name,
                    CreatedAt = DateTime.Now,
                    OwnerEmail = request.OwnerEmail,
                    Members = new List<User>(),
                    Columns = new List<Column>()
                    {
                        new Column()
                        {
                            Name = "Backlog"
                        },
                        new Column()
                        {
                            Name = "To Do"
                        },
                        new Column()
                        {
                            Name = "In Progress"
                        },
                        new Column()
                        {
                            Name = "Testing"
                        },
                        new Column()
                        {
                            Name = "Done",
                        },
                    }
                };

                board.Members.Add(user);

                var errors = new List<string>();

                try
                {
                    await _context.AddAsync(board, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    _logger.LogInformation($"[{DateTime.UtcNow}] Board was created.");
                }
                catch (Exception e)
                {
                    errors.Add(e.Message);
                    _logger.LogError(string.Join(Environment.NewLine, errors));
                    return Result.BadRequest<Board>(errors);
                }

                return Result.Ok();
            }
        }
    }
}
