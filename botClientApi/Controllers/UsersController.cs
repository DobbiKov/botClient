using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using botClientApi;
using botClientApi.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using botClientApi.Globals;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace botClientApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private int UserId => int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Getusers()
        {
            return await _context.users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _context.users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        [HttpGet("/api/Users/ByToken")]
        [Authorize]
        public async Task<ActionResult<User>> GetUserByToken()
        {
            var id = UserId;
            var user = await _context.users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return NotFound();
            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var checkUser = await _context.users.FirstOrDefaultAsync(x => x.Username == user.Username);
            if(checkUser != null)
            {
                return BadRequest(new { errorText = "Пользователь с таким никнеймом уже имеется." });
            }

            _context.users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(long id)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }
        [HttpPost("/api/Users/Auth")]
        public async Task<ActionResult> Auth(User user)
        {
            var _user = await _context.users.FirstOrDefaultAsync(x => x.Username == user.Username && x.Password == user.Password);
            if (_user == null)
                return NotFound();
            var token = GenerateJWTToken(user);
            return Ok(new { acces_token = token });
        }
        private string GenerateJWTToken(User user)
        {
            var securityKey = authJwtOptions.SecretKey;
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email, user.Username),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            var token = new JwtSecurityToken(authJwtOptions.Issuer, authJwtOptions.Audience, claims, expires: DateTime.Now.AddSeconds(3600), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private bool UserExists(long id)
        {
            return _context.users.Any(e => e.Id == id);
        }
    }
}
