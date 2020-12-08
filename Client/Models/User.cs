﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class User
    {

        public int UserId { get; set; }
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
