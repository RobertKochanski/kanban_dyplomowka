using KanbanAPI.Extensions;
using KanbanBAL.CQRS.Commands.Users;
using KanbanDAL.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace KanbanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;

        public UsersController(IMediator mediator, UserManager<User> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            return await _mediator.Send(command).Process();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            var user = new User
            {
                Email = command.Email,
                UserName = command.UserName
            };

            command.Link = Url.Action("ConfirmEmail", "Users", new { username = user.UserName }, Request.Scheme);

            return await _mediator.Send(command).Process();
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery]string username)
        {
            await _mediator.Send(new ConfirmEmailCommand(username)).Process();
            return Redirect("http://localhost:4200");
        }

        [HttpGet("SendConfirm")]
        public async Task<IActionResult> SendConfirmEmail([FromQuery] string username)
        {
            var user = new User
            {
                UserName = username
            };

            var xd = new SendConfirmEmailCommand();

            xd.Username = username;
            xd.Link = Url.Action("ConfirmEmail", "Users", new { username = user.UserName }, Request.Scheme);

            return await _mediator.Send(xd).Process();
        }
    }
}
