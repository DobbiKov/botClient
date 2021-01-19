using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using botClientApi;
using botClientApi.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Telegram.Bot;

namespace botClientApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MessagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> Getmessages()
        {
            return await _context.messages.ToListAsync();
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(Guid id)
        {
            var message = await _context.messages.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }
        [HttpGet("/api/Messages/ByChatId/{id}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesByChatId(long id)
        {
            var message = await _context.messages.ToListAsync();

            foreach(var mess in message.ToList())
            {
                if(mess.Telegramchatid != id)
                {
                    message.Remove(mess);
                }
            }

            return message;
        }

        // PUT: api/Messages/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(Guid id, Message message)
        {
            if (id != message.Id)
            {
                return BadRequest();
            }

            _context.Entry(message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
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

        // POST: api/Messages
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(Message message)
        {
            if (message.Telegramuserid == Config.BotId)
            {
                message.Username = "dobbikovbot";
                message.Firstname = "Роман";
                message.Lastname = "Запотоцкий";
                message.Messageid = 1;
            }
            var chat = await _context.chats.FirstOrDefaultAsync(x => x.Telegramid == message.Telegramchatid);
            if(chat == null)
            {
                Chat newChat = new Chat();
                newChat.Telegramid = message.Telegramchatid;
                newChat.Username = message.Username;
                newChat.Firstname = message.Firstname;
                newChat.Lastname = message.Lastname;
                /*                var json = JsonConvert.SerializeObject(newChat);
                                var data = new StringContent(json, Encoding.UTF8, "application/json");
                                var http = new HttpClient();
                                var response = await http.PostAsync($"{Config.Url}/api/Chats", data);*/
                var chatcont = new ChatsController(_context);
                await chatcont.PostChat(newChat);
                chat = newChat;
            }
            message.Chatid = chat.Id;
            _context.messages.Add(message);
            await _context.SaveChangesAsync();

            if(message.Telegramuserid == Config.BotId)
            {
                var client = new TelegramBotClient(Config.Token);
                await client.SendTextMessageAsync(message.Telegramchatid, message.Text);
                /*botClientBot.Program.SendMessage(message.Telegramchatid, message.Text);*/
            }

            return CreatedAtAction("GetMessage", new { id = message.Id }, message);
        }

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Message>> DeleteMessage(Guid id)
        {
            var message = await _context.messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.messages.Remove(message);
            await _context.SaveChangesAsync();

            return message;
        }

        private bool MessageExists(Guid id)
        {
            return _context.messages.Any(e => e.Id == id);
        }
    }
}
