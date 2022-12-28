using KanbanAPI.Extensions;
using KanbanBAL.CQRS.Commands.Comments;
using KanbanBAL.CQRS.Queries.Comments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KanbanAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{jobId}")]
        public async Task<IActionResult> Get(Guid jobId)
        {
            return await _mediator.Send(new GetCommentsQuery(jobId)).Process();
        }

        [HttpPost("{jobId}")]
        public async Task<IActionResult> Post(Guid jobId, CreateCommentCommand command)
        {
            command.JobId = jobId;
            command.Creator = User.FindFirstValue(ClaimTypes.Email);
            return await _mediator.Send(command).Process();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await _mediator.Send(new DeleteCommentCommand(id)).Process();
        }
    }
}
