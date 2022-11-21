using KanbanBAL.Authentication;
using KanbanBAL.Results;
using KanbanDAL.Entities;
using KanbanDAL.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace KanbanBAL.CQRS.Commands.Users
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<ResponseUserModel>>
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

        public async Task<Result<ResponseUserModel>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                _logger.LogError($"[{DateTime.UtcNow}] Wrong email or password!");
                return Result.BadRequest<ResponseUserModel>(new List<string> { $"Wrong email or password!" });
            }

            if (!await _userManager.CheckPasswordAsync(user, request.Password))
            {
                _logger.LogError($"[{DateTime.UtcNow}] Wrong email or password!");
                return Result.BadRequest<ResponseUserModel>(new List<string> { $"Wrong email or password!" });
            }

            var token = _token.CreateToken(user);

            var result = new ResponseUserModel()
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Token = token,
            };

            return Result.Ok(result);
        }
    }
}
