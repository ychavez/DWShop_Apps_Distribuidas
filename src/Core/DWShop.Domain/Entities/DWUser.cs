using Microsoft.AspNetCore.Identity;

namespace DWShop.Domain.Entities
{
    public class DWUser : IdentityUser<string>
    {
        public string Gafette { get; set; }
    }
}
