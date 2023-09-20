using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ireview.Core.Model
{
    public class Stars
    {
        [Key]
        public int Id { get; set; } 
        public int Amount { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public Article Article { get; set; }
        public int ArticleId { get; set; }
    }
}
