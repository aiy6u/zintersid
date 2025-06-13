using Microsoft.AspNetCore.Identity;

namespace zintersid.Data.Entities
{
    public class AppUserToken : IdentityUserToken<string>
    {
        public DateTime? Expiration { get; set; }
    }
}