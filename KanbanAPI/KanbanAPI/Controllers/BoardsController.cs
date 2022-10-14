using KanbanAPI.Extensions;
using KanbanBAL.CQRS.Commands.Boards;
using KanbanBAL.CQRS.Queries.Boards;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("All")]
        public async Task<IActionResult> GetAll([FromQuery] GetBoardsQuery query)
        {
            return await _mediator.Send(query).Process();
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
            command.OwnerId = User.Identity.Name;
            return await _mediator.Send(command).Process();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateBoardCommand command)
        {
            command.Id = id;
            command.UserId = User.Identity.Name;
            return await _mediator.Send(command).Process();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await _mediator.Send(new DeleteBoardCommand(id, User.Identity.Name)).Process();
        }
    }
}
