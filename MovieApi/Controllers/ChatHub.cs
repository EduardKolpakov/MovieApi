using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MovieApi.DataBaseContext;
using MovieApi.Model;
using System;

namespace MovieApi.Controllers
{
    public class ChatHub : Hub
    {
        private readonly ContextDb _context;

        public ChatHub(ContextDb context)
        {
            _context = context;
        }

        // Отправка сообщения в чат фильма
        public async Task SendMovieMessage(int movieId, string userName, string message)
        {
            var msg = new MovieChatMessage
            {
                MovieId = movieId,
                UserName = userName,
                Message = message
            };

            _context.MovieChatMessages.Add(msg);
            await _context.SaveChangesAsync();

            await Clients.Group($"movie_{movieId}").SendAsync("ReceiveMovieMessage", userName, message, msg.Timestamp.ToString("HH:mm"));
        }

        // Присоединение к чату фильма
        public async Task JoinMovieChat(int movieId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"movie_{movieId}");

            // Загрузить последние сообщения
            var history = await _context.MovieChatMessages
                .Where(m => m.MovieId == movieId)
                .OrderByDescending(m => m.Timestamp)
                .Take(50)
                .ToListAsync();

            foreach (var msg in history)
            {
                await Clients.Caller.SendAsync("ReceiveMovieMessage", msg.UserName, msg.Message, msg.Timestamp.ToString("o"));
            }
        }

        public async Task LeaveMovieChat(int movieId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"movie_{movieId}");
        }

        // Отправка личного сообщения
        public async Task SendPrivateMessage(string fromId, string toId, string fromName, string message)
        {
            var msg = new PrivateChatMessage
            {
                SenderId = fromId,
                ReceiverId = toId,
                SenderName = fromName,
                Message = message
            };

            _context.PrivateChatMessages.Add(msg);
            await _context.SaveChangesAsync();

            await Clients.Group($"{GetPrivateChatId(fromId, toId)}")
                         .SendAsync("ReceivePrivateMessage", fromId, fromName, message, msg.Timestamp.ToString("HH:mm"));
        }
        public async Task JoinPrivateChat(string userId, string otherUserId)
        {
            var chatId = GetPrivateChatId(userId, otherUserId);
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
            var history = await _context.PrivateChatMessages
                .Where(m => (m.SenderId == userId && m.ReceiverId == otherUserId) ||
                            (m.SenderId == otherUserId && m.ReceiverId == userId))
                .OrderByDescending(m => m.Timestamp)
                .Take(50)
                .ToListAsync();

            foreach (var msg in history)
            {
                await Clients.Caller.SendAsync("ReceivePrivateMessage", msg.SenderId,
                    msg.SenderName, msg.Message, msg.Timestamp.ToString("HH:mm"));
            }
        }

        // Генерация уникального ID для приватного чата
        private string GetPrivateChatId(string id1, string id2)
        {
            return string.Compare(id1, id2) < 0 ? $"private_{id1}_{id2}" : $"private_{id2}_{id1}";
        }
    }
}
