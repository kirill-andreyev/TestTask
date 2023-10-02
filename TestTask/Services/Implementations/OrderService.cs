using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        
        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrder()
        {
            var allOrders = await  _context.Orders.ToListAsync();
            return allOrders.MaxBy(x => x.Price * x.Quantity); //Returns the order with the highest amount. If more than one returns the first
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _context.Orders.Where(x => x.Quantity > 10).ToListAsync();
        }
    }
}
