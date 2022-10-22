using KanbanAPI.Extensions;
using KanbanBAL.CQRS.Commands.Columns;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanbanAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ColumnsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{boardId}")]
        public async Task<IActionResult> Post(Guid boardId, CreateColumnCommand command)
        {
            command.BoardId = boardId;
            return await _mediator.Send(command).Process();
        }

        [HttpPut("{columnId}")]
        public async Task<IActionResult> Put(Guid columnId, UpdateColumnCommand command)
        {
            command.ColumnId = columnId;
            return await _mediator.Send(command).Process();
        }

        [HttpDelete("{columnId}")]
        public async Task<IActionResult> Delete(Guid columnId)
        {
            return await _mediator.Send(new DeleteColumnCommand(columnId)).Process();
        }
    }
}
