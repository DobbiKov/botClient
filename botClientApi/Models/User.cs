﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace botClientApi.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
