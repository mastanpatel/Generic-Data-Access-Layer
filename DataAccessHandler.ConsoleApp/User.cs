using System;

namespace DataAccessHandler.ConsoleApp
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public bool IsActive { get; set; } 
    }
}
