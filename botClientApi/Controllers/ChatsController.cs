using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using botClientApi;
using botClientApi.Models;

namespace botClientApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Chats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chat>>> Getchats()
        {
            return await _context.chats.ToListAsync();
        }

        // GET: api/Chats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chat>> GetChat(Guid id)
        {
            var chat = await _context.chats.FindAsync(id);

            if (chat == null)
            {
                return NotFound();
            }

            return chat;
        }

        // PUT: api/Chats/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChat(Guid id, Chat chat)
        {
            if (id != chat.Id)
            {
                return BadRequest();
            }

            _context.Entry(chat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Chats
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Chat>> PostChat(Chat chat)
        {
            _context.chats.Add(chat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChat", new { id = chat.Id }, chat);
        }

        // DELETE: api/Chats/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Chat>> DeleteChat(Guid id)
        {
            var chat = await _context.chats.FindAsync(id);
            if (chat == null)
            {
                return NotFound();
            }

            _context.chats.Remove(chat);
            await _context.SaveChangesAsync();

            return chat;
        }

        private bool ChatExists(Guid id)
        {
            return _context.chats.Any(e => e.Id == id);
        }
    }
}
