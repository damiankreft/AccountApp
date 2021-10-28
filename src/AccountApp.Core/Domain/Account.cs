using System;

namespace AccountApp.Core.Domain
{
    public class Account
    {
        public Guid Id { get;  set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get;  set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string RegistrationEmail { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string LastIp { get;  set;}
        public string LastAttemptIp { get;  set; }
        public string Os { get; set; }

        public Account() {} 

        public Account(Guid id, string email, string username, string password, string salt, string role)
        {
            Id = id;
            Email = email;
            SetUsername(username);
            Password = password;
            Salt = salt;
            Role = role;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetUsername(string username)
        {
            if (String.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username is null or empty.");
            }

            Username = username.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }
    }
}