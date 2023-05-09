using KanbanBAL.Email;
using KanbanBAL.Results;
using KanbanDAL.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Duende.IdentityServer.Models.IdentityResources;

namespace KanbanBAL.CQRS.Commands.Users
{
    public class SendConfirmEmailCommand : IRequest<Result>
    {
        public string? Username { get; set; }
        [JsonIgnore]
        public string? Link { get; set; }
    }

    public class SendConfirmEmailCommandHandler : IRequestHandler<SendConfirmEmailCommand, Result>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<SendConfirmEmailCommandHandler> _logger;
        private readonly IEmailHelper _emailHelper;

        public SendConfirmEmailCommandHandler(UserManager<User> userManager, ILogger<SendConfirmEmailCommandHandler> logger, IEmailHelper emailHelper)
        {
            _userManager = userManager;
            _emailHelper = emailHelper;
            _logger = logger;
            _logger.LogInformation($"[{DateTime.UtcNow}] Object '{nameof(SendConfirmEmailCommandHandler)}' has been created.");
        }

        public async Task<Result> Handle(SendConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            if (request.Username == null)
            {
                _logger.LogError($"Fill username field");
                return Result.BadRequest($"Fill username field");
            }

            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                _logger.LogError($"The Username {request.Username} is invalid");
                return Result.BadRequest($"The Username {request.Username} is invalid");
            }

            var text = $"<strong>Hello {user.UserName} 👋</strong><br> " +
                $"Welcome to Kanban, new friend!<br><br>" +
                $"To get your account ready for work, we need you to confirm that this email belongs to you: " +
                $"<a href={request.Link} class=\"btn btn-primary\">Go to Google</a>" +
                "<a href=\"{{{pm:unsubscribe }}}\"></a>";

//Hi Robert Kochański 👋
//Welcome to Postmark, new friend! Your username to log in is Vegittoo.

//To get your account ready for sending, we’ve created a Sender Signature for naruto18 @wp.pl.
//We’ll use that email address as the “From” address on the emails you send.But before we can do that,
//we need you to confirm that this email belongs to you:

            return await _emailHelper.sendEmail(user.UserName, request.Link, text);
        }
    }
}
