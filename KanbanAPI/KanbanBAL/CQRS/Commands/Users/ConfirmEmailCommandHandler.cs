using KanbanBAL.Results;
using KanbanDAL.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace KanbanBAL.CQRS.Commands.Users
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ConfirmEmailCommandHandler> _logger;

        public ConfirmEmailCommandHandler(UserManager<User> userManager, ILogger<ConfirmEmailCommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(ConfirmEmailCommandHandler)}' has been created.");
        }

        public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByNameAsync(request.username);
            if (user == null)
            {
                _logger.LogError($"The Username {request.username} is invalid");
                return Result.BadRequest($"The Username {request.username} is invalid");
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Result.Ok();
            }

            _logger.LogError("Email cannot be confirmed");
            return Result.BadRequest("Email cannot be confirmed");
        }
    }
}
