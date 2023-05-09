using KanbanBAL.Results;
using KanbanDAL.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PostmarkDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanBAL.Email
{
    public class EmailHelper : IEmailHelper
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public EmailHelper(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<Result> sendEmail(string username, string link, string text)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return Result.BadRequest($"Can not find user with username: {username}");
            }

            // Send an email asynchronously:
            var message = new PostmarkMessage()
            {
                To = $"{user.Email}",
                From = "kochanskirobert1999@wp.pl",
                Subject = "Confirm email - Kanban",
                HtmlBody = text,
                MessageStream = "broadcast",
                Tag = "Register",
            };

            var client = new PostmarkClient(_configuration.GetValue<string>("PostmarkAPI"));
            var sendResult = await client.SendMessageAsync(message);

            if (sendResult.Status == PostmarkStatus.Success)
            {
                return Result.Ok();
            }
            else
            {
                await _userManager.DeleteAsync(user);
                return Result.BadRequest("Can not send mail with confirm");
            }
        }
    }
}
