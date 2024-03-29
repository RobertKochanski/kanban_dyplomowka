﻿namespace KanbanDAL.Models
{
    public class ResponseJobModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public IEnumerable<string>? UserEmails { get; set; }
        public Guid ColumnId { get; set; }
        public DateTime? Deadline { get; set; }
        public string Priority { get; set; }
    }
}
