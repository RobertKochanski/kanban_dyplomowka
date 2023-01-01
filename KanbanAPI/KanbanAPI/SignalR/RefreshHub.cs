using KanbanDAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace KanbanAPI.SignalR
{
    [Authorize]
    public class RefreshHub: Hub
    {
        private readonly KanbanDbContext _context;

        public RefreshHub(KanbanDbContext context)
        {
            _context = context;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var board = httpContext.Request.Query["board"];
            var groupName = board;

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.Others.SendAsync("UserIsOffline", Context.User.FindFirstValue(ClaimTypes.Email));

            await base.OnDisconnectedAsync(exception);
        }
    }
}
