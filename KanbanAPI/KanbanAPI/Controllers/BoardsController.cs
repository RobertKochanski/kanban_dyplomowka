using KanbanAPI.Extensions;
using KanbanBAL.CQRS.Commands.Boards;
using KanbanBAL.CQRS.Queries.Boards;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KanbanAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BoardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("UserAll")]
        public async Task<IActionResult> GetAllUserBoards()
        {
            return await _mediator.Send(new GetUserBoardsQuery(User.Identity.Name)).Process();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return await _mediator.Send(new GetBoardDetailsQuery(id)).Process();
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateBoardCommand command)
        {
            command.OwnerEmail = User.FindFirstValue(ClaimTypes.Email);
            return await _mediator.Send(command).Process();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateBoardCommand command)
        {
            command.Id = id;
            command.UserEmail = User.FindFirstValue(ClaimTypes.Email);
            return await _mediator.Send(command).Process();
        }

        [HttpPut("{boardId}/{userId}")]
        public async Task<IActionResult> Put(Guid boardId, string userId, RemoveUserFromBoardCommand command)
        {
            command.BoardId = boardId;
            command.UserId = userId;
            return await _mediator.Send(command).Process();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await _mediator.Send(new DeleteBoardCommand(id, User.FindFirstValue(ClaimTypes.Email))).Process();
        }
    }
}
