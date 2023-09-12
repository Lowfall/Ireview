using Ireview.Core.Mapping;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ireview.Infrastructure.Identity.Models
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? Gender { get; set; }
        public DateTime? RegisterDate { get; set; }
    }
}
