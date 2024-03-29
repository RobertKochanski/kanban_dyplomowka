﻿namespace KanbanDAL.Models
{
    public class ResponseBoardModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<ResponseColumnModel>? Columns { get; set; }

        public string OwnerEmail { get; set; }
        public IEnumerable<ResponseUserModel> Members { get; set; }

        public DateTime CurrentDate { get; set; }
    }
}
