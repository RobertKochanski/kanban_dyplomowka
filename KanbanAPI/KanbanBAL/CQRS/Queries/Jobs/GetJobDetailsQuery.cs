using KanbanBAL.CQRS.Queries.Boards;
using KanbanBAL.Results;
using KanbanDAL.Entities;
using KanbanDAL.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanBAL.CQRS.Queries.Jobs
{
    public class GetJobDetailsQuery : IRequest<Result<Job>>
    {
        public Guid Id { get; set; }

        public GetJobDetailsQuery(Guid id)
        {
            Id = id;
        }
    }
}
