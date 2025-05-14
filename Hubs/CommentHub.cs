using Microsoft.AspNetCore.SignalR;
using ProductCommentApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ProductCommentApp.Hubs
{
    public class CommentHub : Hub
    {
        private readonly AppDbContext _context;

        public CommentHub(AppDbContext context)
        {
            _context = context;
        }

        public async Task SendComment(int productId, string username, string text)
        {
            var comment = new Comment
            {
                ProductId = productId,
                Username = username,
                Text = text
            };
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            await Clients.Group($"product_{productId}").SendAsync("ReceiveComment", username, text);
            await Clients.All.SendAsync("ReceiveNotification", username, productId);
        }

        public async Task JoinProductGroup(int productId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"product_{productId}");
        }

        public async Task LeaveProductGroup(int productId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"product_{productId}");
        }

        public async Task BuyProduct(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null && product.Quantity > 0)
            {
                product.Quantity -= 1;
                await _context.SaveChangesAsync();

                // إرسال التحديث للكل
                await Clients.All.SendAsync("UpdateQuantity", productId, product.Quantity);
            }
        }
    }
}