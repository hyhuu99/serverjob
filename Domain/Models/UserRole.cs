﻿using System;

namespace Domain.Models
{
    public class UserRole : IAggregateRoot
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}
