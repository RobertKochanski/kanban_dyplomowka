using KanbanBAL.Results;

namespace KanbanBAL.Email
{
    public interface IEmailHelper
    {
        Task<Result> sendEmail(string username, string link, string text);
    }
}