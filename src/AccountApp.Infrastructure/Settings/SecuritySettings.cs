namespace AccountApp.Infrastructure.Settings
{
    public class SecuritySettings
    {
        public string[] CorsAllowedOrigins = { "http://localhost:4200" }; 
        public string JwtValidIssuer { get; set; }
    }
}