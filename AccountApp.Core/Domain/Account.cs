namespace AccountApp.Core.Domain
{
    public class Account
    {
        public int Id { get; protected set; }
        public string Username { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string Role { get; protected set; }
        public string Email { get; set; }
        public string RegistrationEmail { get; set; }
        public string LastIp { get; protected set;}
        public string LastAttemptIp { get; protected set; }
        public string Os { get; protected set; }

        public Account(string email, string username, string password, string salt, string role)
        {
            Email = email;
            Username = username;
            Password = password;
            Salt = salt;
            Role = role;
        }
    }
}