using KanbanBAL.CQRS.Queries.Boards;
using KanbanDAL;
using KanbanDAL.Entities;
using KanbanDAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading;

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
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == Context.User.Identity.Name);

            var boards = await _context.Boards
                .Include(x => x.Columns)
                    .ThenInclude(x => x.Jobs)
                        .ThenInclude(x => x.Users)
                .Include(x => x.Members)
                .Where(x => x.Members.Contains(user))
                .Select(x => new ResponseBoardModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedAt = x.CreatedAt,
                    Columns = x.Columns.Select(y => new ResponseColumnModel
                    {
                        Id = y.Id,
                        BoardId = y.BoardId,
                        Name = y.Name,
                        Jobs = y.Jobs.Select(z => new ResponseJobModel
                        {
                            Id = z.Id,
                            Name = z.Name,
                            Description = z.Description,
                            UserEmails = z.Users.Select(em => em.Email)
                        })
                    }),
                    OwnerEmail = x.OwnerEmail,
                    Members = x.Members.Select(y => new ResponseUserModel
                    {
                        Id = y.Id,
                        Email = y.Email,
                        Username = y.UserName
                    })
                })
                .ToListAsync();

            await Clients.Others.SendAsync("Refresh", boards);
        }
    }
}
