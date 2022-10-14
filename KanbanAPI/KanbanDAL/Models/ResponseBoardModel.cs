using KanbanDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanDAL.Models
{
    public class ResponseBoardModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Column>? Columns { get; set; }

        public string OwnerId { get; set; }
        public IEnumerable<ResponseMemberModel> Members { get; set; }
    }
}
