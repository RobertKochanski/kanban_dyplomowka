using KanbanBAL.Authentication;
using KanbanBAL.Results;
using KanbanDAL.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace KanbanBAL.CQRS.Commands
{
    public class RegisterUserCommand : IRequest<Result<string>>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<string>>
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterUserCommandHandler> _logger;

        public RegisterUserCommandHandler(ITokenGenerator tokenGenerator, UserManager<User> userManager, ILogger<RegisterUserCommandHandler> logger)
        {
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(RegisterUserCommandHandler)}' has been created.");
        }

        public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.ConfirmPassword))
            {
                _logger.LogError($"[{DateTime.UtcNow}] Fill all fields!");
                return Result.BadRequest<string>(new List<string> { "Fill all fields!" });
            }

            if (request.Password != request.ConfirmPassword)
            {
                _logger.LogError($"[{DateTime.UtcNow}] Passwords don't match!");
                return Result.BadRequest<string>(new List<string> { "Passwords don't match!" });
            }

            var user = new User
            {
                Email = request.Email,
                UserName = request.UserName
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                _logger.LogError(string.Join(" ", result.Errors.Select(x => x.Description)));
                return Result.BadRequest<string>(result.Errors.Select(x => x.Description).ToList());
            }

            var token = _tokenGenerator.CreateToken(user);

            return Result.Ok(token);
        }
    }
}
