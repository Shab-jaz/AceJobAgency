﻿using Microsoft.AspNetCore.Identity;

namespace AceJobAgency.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string NRIC { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ResumePath { get; set; }
        public string WhoamI { get; set; }
        
    }
}
