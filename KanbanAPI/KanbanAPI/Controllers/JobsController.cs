using KanbanAPI.Extensions;
using KanbanBAL.CQRS.Commands.Jobs;
using KanbanBAL.CQRS.Queries.Jobs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanbanAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return await _mediator.Send(new GetJobDetailsQuery(id)).Process();
        }

        [HttpPost("{columnId}")]
        public async Task<IActionResult> Post(Guid columnId, CreateJobCommand command)
        {
            command.ColumnId = columnId;
            return await _mediator.Send(command).Process();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateJobCommand command)
        {
            command.Id = id;
            return await _mediator.Send(command).Process();
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    return await _mediator.Send(new DeleteJobCommand(id)).Process();
        //}
    }
}
