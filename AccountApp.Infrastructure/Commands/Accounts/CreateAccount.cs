namespace AccountApp.Infrastructure.Commands.Accounts
{
    public class CreateAccount : ICommand
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}