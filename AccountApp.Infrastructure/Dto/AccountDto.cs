namespace AccountApp.Infrastructure.Dto
{
    public class AccountDto
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public AccountDto() { }

        public AccountDto(string email, string password)
        {
            Email = email;
            PasswordHash = password;
        }
    }
}