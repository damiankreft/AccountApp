namespace AccountApp.Infrastructure.Dto
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; init; }
        public string LastAttemptIp { get; set; }

        public AccountDto() { }

        public AccountDto(string email, string role, int id, string username, string lastAttemptIp)
        {
            Email = email;
            Role = role;
            Id = id;
            Username = username;
            LastAttemptIp = lastAttemptIp;
        }
    }
}