﻿using Microsoft.AspNetCore.Identity;

namespace CafeManagement.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Role {  get; set; }
    }
}
