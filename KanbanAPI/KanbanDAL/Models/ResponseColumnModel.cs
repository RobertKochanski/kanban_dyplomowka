using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanDAL.Models
{
    public class ResponseColumnModel
    {
        public Guid Id { get; set; }
        public Guid BoardId { get; set; }
        public string Name { get; set; }
        public IEnumerable<ResponseJobModel> Jobs { get; set; }
    }
}
