using Microsoft.AspNetCore.Identity;

namespace Eden.Core.Entities.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public Guid UniqueID { get; set; } = Guid.NewGuid();
    }
}
