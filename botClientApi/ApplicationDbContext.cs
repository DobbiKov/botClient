using botClientApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace botClientApi
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Chat> chats { get; set; }
        public DbSet<Message>  messages { get; set; }
        public DbSet<User> users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
    }
}
