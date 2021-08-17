namespace AccountApp.Infrastructure.Dto
{
    public class AccountDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Role { get; init; }

        public AccountDto() { }

        public AccountDto(string email, string password, string salt, string role)
        {
            Email = email;
            Password = password;
            Salt = salt;
            Role = role;
        }
    }
}