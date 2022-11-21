using KanbanBAL.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace KanbanBAL.CQRS.Commands.Boards
{
    public class CreateBoardCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public bool InitialSettings { get; set; }
        [JsonIgnore]
        public string? OwnerEmail { get; set; }

        public CreateBoardCommand(string name, string ownerEmail, bool initialSettings)
        {
            Name = name;
            OwnerEmail = ownerEmail;
            InitialSettings = initialSettings;
        }
    }
}
