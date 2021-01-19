using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace botClientApi.Models
{
    public class Chat
    {
        public Guid Id { get; set; }
        public long Telegramid { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
