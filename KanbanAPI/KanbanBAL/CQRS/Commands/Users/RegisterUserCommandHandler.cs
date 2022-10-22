using KanbanBAL.Authentication;
using KanbanBAL.Results;
using KanbanDAL.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace KanbanBAL.CQRS.Commands.Users
{
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
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(request.Email) 
                || string.IsNullOrEmpty(request.UserName) 
                || string.IsNullOrEmpty(request.Password) 
                || string.IsNullOrEmpty(request.ConfirmPassword))
            {
                errors.Add("Fill all fields!");
            }

            var userEmailCheck = await _userManager.FindByEmailAsync(request.Email);

            if (userEmailCheck != null)
            {
                errors.Add($"Email '{request.Email}' is already taken.");
            }

            if (request.Password != request.ConfirmPassword)
            {
                errors.Add("Passwords don't match!");
            }

            var user = new User
            {
                Email = request.Email,
                UserName = request.UserName
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                errors.AddRange(result.Errors.Select(x => x.Description).ToList());
            }

            if (errors.Count > 0)
            {
                _logger.LogError(string.Join(Environment.NewLine, errors));
                return Result.BadRequest<string>(errors);
            }

            var token = _tokenGenerator.CreateToken(user);

            return Result.Ok(token);
        }
    }
}
