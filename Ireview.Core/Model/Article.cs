using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ireview.Core.Model
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Header { get; set; }
        public string Group { get; set; }
        public string? ImageSource { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Tag>? Tag { get; set; }
        public User User { get; set; }
        public int? Rating { get; set; }
        public int Stars { get; set; }
       

    }
}
