namespace AccountApp.Core.Domain
{
    public class Account
    {
        public int Id { get;  set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get;  set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string RegistrationEmail { get; set; }
        public string LastIp { get;  set;}
        public string LastAttemptIp { get;  set; }
        public string Os { get; set; }

        public Account() {} 

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