﻿using KanbanBAL.Results;
using KanbanDAL.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace KanbanBAL.CQRS.Commands.Columns
{
    public class MovingJobsCommand : IRequest<Result>
    {
        public MovingJobsCommandObject obj { get; set; }
    }

    public class MovingJobsCommandObject
    {
        public Guid currentColumnId { get; set; }
        public List<ResponseJobModel>? currentContainer { get; set; }
    }
}
