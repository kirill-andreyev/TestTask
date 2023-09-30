using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        async Task<User> IUserService.GetUser()
        {
            var allUsers = _context.Orders.GroupBy(x => x.UserId).Select(g => new
            {
                UserId = g.Key,
                MaxNumberOfOrders = g.Count()
            }).OrderByDescending(x => x.MaxNumberOfOrders).FirstOrDefault();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == allUsers.UserId);
            return user;
        }

        async Task<List<User>> IUserService.GetUsers()
        {
            return await _context.Users.Where(x => x.Status == Enums.UserStatus.Inactive).ToListAsync();
        }
    }
}
