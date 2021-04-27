namespace AccountApp.Core.Domain
{
    public class Account
    {
        public int Id { get; protected set; }
        public string Username { get; protected set; }
        public string PasswordHash { get; protected set; }
        public string Email { get; set; }
        public string RegistrationEmail { get; set; }
        public string LastIp { get; protected set;}
        public string LastAttemptIp { get; protected set; }
        public string Os { get; protected set; }
        public int Recruiter { get; protected set; }

        public Account(string email, string password)
        {
            Email = email;
            PasswordHash = password;
        }
    }
}