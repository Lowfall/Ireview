using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Required]
        public string Title { get; set; }
        [Required]
        public string Header { get; set; }
        public string Group { get; set; }
        public string ImageSource { get; set; }
        public string? ImagePublicId { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Tag>? Tag { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public float? Rating { get; set; }
        public ICollection<Stars> Stars { get; set; }
    }
}
