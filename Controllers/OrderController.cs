using E_Commerce.Data;
using E_Commerce.Dtos.Order;
using E_Commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly MyDbContext _dbContext;

        public OrderController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _dbContext.Orders
                                         .Include(o => o.OrderItems)
                                         .ThenInclude(oi => oi.Product)
                                         .Where(o => o.UserId == userId)
                                         .ToListAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _dbContext.Orders
                                        .Include(o => o.OrderItems)
                                        .ThenInclude(oi => oi.Product)
                                        .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderRequest addOrderRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                OrderItems = addOrderRequest.OrderItems.Select(oi => new OrderItem
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            };

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            return Ok(order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] int id, OrderRequest updateOrderRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _dbContext.Orders
                                        .Include(o => o.OrderItems)
                                        .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);
            if (order == null)
            {
                return NotFound();
            }

            // Remove existing order items
            _dbContext.OrderItems.RemoveRange(order.OrderItems);

            // Add new order items
            order.OrderItems = updateOrderRequest.OrderItems.Select(oi => new OrderItem
            {
                OrderId = id,
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice
            }).ToList();

            order.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            return Ok(order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _dbContext.Orders
                                        .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);
            if (order == null)
            {
                return NotFound();
            }

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
