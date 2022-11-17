using KanbanBAL.Authentication;
using KanbanBAL.Results;
using KanbanDAL.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace KanbanBAL.CQRS.Commands.Users
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<string>>
    {
        private readonly ITokenGenerator _token;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<LoginUserCommandHandler> _logger;

        public LoginUserCommandHandler(ILogger<LoginUserCommandHandler> logger, UserManager<User> userManager, ITokenGenerator token)
        {
            _token = token;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                _logger.LogError($"[{DateTime.UtcNow}] Wrong email or password!");
                return Result.BadRequest<string>(new List<string> { $"Wrong email or password!" });
            }

            if (!await _userManager.CheckPasswordAsync(user, request.Password))
            {
                _logger.LogError($"[{DateTime.UtcNow}] Wrong email or password!");
                return Result.BadRequest<string>(new List<string> { $"Wrong email or password!" });
            }

            var token = _token.CreateToken(user);

            return Result.Ok(token);
        }
    }
}
