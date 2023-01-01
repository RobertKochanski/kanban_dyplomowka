﻿using KanbanDAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace KanbanAPI.SignalR
{
    [Authorize]
    public class PresenceHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.Others.SendAsync("UserIsOnline", Context.User.FindFirstValue(ClaimTypes.Email));
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.Others.SendAsync("UserIsOffline", Context.User.FindFirstValue(ClaimTypes.Email));

            await base.OnDisconnectedAsync(exception);
        }
    }
}
