namespace AccountApp.Infrastructure.Dto
{
    public class JwtDto
    {
        public string Token { get; set; }
        public long Expiry { get; set; }
    }
}