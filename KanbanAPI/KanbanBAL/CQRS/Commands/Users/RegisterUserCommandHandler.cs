using KanbanBAL.Authentication;
using KanbanBAL.Results;
using KanbanDAL;
using KanbanDAL.Entities;
using KanbanDAL.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace KanbanBAL.CQRS.Commands.Users
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<ResponseUserModel>>
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly UserManager<User> _userManager;
        private readonly KanbanDbContext _context;
        private readonly ILogger<RegisterUserCommandHandler> _logger;

        public RegisterUserCommandHandler(ITokenGenerator tokenGenerator, UserManager<User> userManager, ILogger<RegisterUserCommandHandler> logger, KanbanDbContext context)
        {
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
            _context = context;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(RegisterUserCommandHandler)}' has been created.");
        }

        public async Task<Result<ResponseUserModel>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
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

            var userUsernameCheck = await _userManager.FindByNameAsync(request.UserName);

            if (userUsernameCheck != null)
            {
                errors.Add($"Username '{request.UserName}' is already taken.");
            }

            if (request.Password != request.ConfirmPassword)
            {
                errors.Add("Passwords don't match!");
            }

            if (errors.Count > 0)
            {
                _logger.LogError(string.Join(Environment.NewLine, errors));
                return Result.BadRequest<ResponseUserModel>(errors);
            }

            var user = new User
            {
                Email = request.Email,
                UserName = request.UserName
            };

            errors = new List<string>();

            var created = await _userManager.CreateAsync(user, request.Password);

            if (!created.Succeeded)
            {
                errors.AddRange(created.Errors.Select(x => x.Description).ToList());
            }

            if (errors.Count > 0)
            {
                _logger.LogError(string.Join(Environment.NewLine, errors));
                return Result.BadRequest<ResponseUserModel>(errors);
            }

            var token = _tokenGenerator.CreateToken(user);

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
