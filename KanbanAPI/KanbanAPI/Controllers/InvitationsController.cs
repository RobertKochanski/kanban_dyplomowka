using KanbanAPI.Extensions;
using KanbanBAL.CQRS.Commands.Invitations;
using KanbanBAL.CQRS.Queries.Invitations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KanbanAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvitationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("UserInvitations")]
        public async Task<IActionResult> GetUserInvitations()
        {
            return await _mediator.Send(new GetInvitationsQuery(User.Identity.Name)).Process();
        }

        [HttpPost("{BoardId}")]
        public async Task<IActionResult> Create(CreateInvitationCommand command, Guid BoardId)
        {
            command.BoardId = BoardId;
            command.InvitingEmail = User.FindFirstValue(ClaimTypes.Email);
            return await _mediator.Send(command).Process();
        }

        [HttpDelete("{InvitationId}")]
        public async Task<IActionResult> Delete(Guid InvitationId)
        {
            return await _mediator.Send(new DeleteInvitationCommand(InvitationId)).Process();
        }

        [HttpPut("{InvitationId}")]
        public async Task<IActionResult> Put(Guid InvitationId)
        {
            return await _mediator.Send(new AcceptInvitationCommand(InvitationId, User.Identity.Name)).Process();
        }
    }
}
