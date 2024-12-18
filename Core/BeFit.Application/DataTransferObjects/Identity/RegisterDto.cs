﻿using BeFit.Domain.Entities;

namespace BeFit.Application.DataTransferObjects
{
    public class RegisterDto
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public int Age { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public Gender Gender { get; set; }
    }
}
