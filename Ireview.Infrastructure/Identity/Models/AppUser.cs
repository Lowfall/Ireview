using Ireview.Core.Model;
using Microsoft.AspNetCore.Identity;

namespace Ireview.Infrastructure.Identity.Models
{
    public class AppUser : IdentityUser
    {   
           public User User { get; set; }

    }
}
 