namespace AccountApp.Infrastructure.Services
{
    public interface IEncrypter 
    {
        string CreateSalt(string value);
        string CreateHash(string value, string salt);

     }
}