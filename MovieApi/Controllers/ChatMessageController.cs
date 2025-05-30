using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.DataBaseContext;
using System;
using System.Text.Json;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api/movie/chat/message")]
    public class ChatMessageController : ControllerBase
    {
        private readonly ContextDb _context;

        public ChatMessageController(ContextDb context) => _context = context;

        [HttpDelete("{messageId}")]
        public async Task<IActionResult> DeleteMessage(int messageId)
        {
            var message = await _context.MovieChatMessages.FindAsync(messageId);
            if (message == null) return NotFound();

            _context.MovieChatMessages.Remove(message);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{messageId}")]
        public async Task<IActionResult> UpdateMessage(int messageId, [FromBody] TextHandlerClass newText)
        {
            var message = await _context.MovieChatMessages.FindAsync(messageId);

            if (message == null || string.IsNullOrWhiteSpace(newText.newText)) return NotFound();

            message.Message = newText.newText;
            await _context.SaveChangesAsync();
            return Ok();
        }

        public class TextHandlerClass
        { 
            public string newText { get; set; }
        }
    }
}
