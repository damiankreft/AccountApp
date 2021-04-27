namespace AccountApp.Infrastructure.Dto
{
    public class AccountDto
    {
        public string Email { get; set; }
        public string Username { get; set; }

        public AccountDto(string email, string username)
        {
            this.Email = email;
            this.Username = username;

        }
    }
}