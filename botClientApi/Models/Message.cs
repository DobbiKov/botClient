using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace botClientApi.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid Chatid { get; set; }
        public long Telegramuserid { get; set; }
        public long Telegramchatid { get; set; }
        public long Messageid { get; set; }
        public string Text { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
