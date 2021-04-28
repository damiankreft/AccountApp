namespace AccountApp.Infrastructure.Dto
{
    public class AccountDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public AccountDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}