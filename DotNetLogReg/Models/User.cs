﻿namespace DotNetLogReg.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string lastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int IsActive { get; set; } = 1;

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
