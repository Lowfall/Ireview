using Ireview.Core.Mapping;
using Ireview.Core.Model;
using Ireview.Infrastructure.Contexts.Repositories;
using Ireview.Web.Models.Profile;
using MapProfile = AutoMapper.Profile;


namespace Ireview.Web.Models.Articles
{
    public class ArticlePageViewModel : IMapTo<Article>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Header { get; set; }
        public string Group { get; set; }
        public string? ImageSource { get; set; }
        public string? ImagePublicId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public int? Rating { get; set; }
        public int Stars { get; set; }
        public GenericRepository<User> Users { get; set; }

        public void Mapping(MapProfile profile)
        {
            profile.CreateMap<Article, ArticlePageViewModel>();
        }
    }
}
