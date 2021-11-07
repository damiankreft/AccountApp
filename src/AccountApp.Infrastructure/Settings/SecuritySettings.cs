namespace AccountApp.Infrastructure.Settings
{
    public class SecuritySettings
    {
        public string[] CorsAllowedOrigins = { "http://localhost:4200", "http://localhost", "https://localhost", "https://localhost:5001", "http://localhost:5000" };
        public string JwtValidIssuer { get; set; }
    }
}