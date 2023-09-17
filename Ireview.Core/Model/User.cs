using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ireview.Core.Model
{
     public class User
    {
        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }    
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public DateTime? RegisterDate { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
