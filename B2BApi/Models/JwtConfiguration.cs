namespace B2BApi.Models
{
    public class JwtConfiguration
    {
        public string SecurityKey { get; set; }
        
        public string Issuer { get; set; }
        
        public int AccessExpiresMinutes { get; set; }

        public int RefreshExpiresMinutes { get; set; }
    }
}