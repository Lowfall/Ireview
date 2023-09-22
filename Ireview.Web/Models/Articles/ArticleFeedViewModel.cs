using Ireview.Core.Mapping;
using Ireview.Core.Model;
using Ireview.Web.Models.Profile;
using MapProfile = AutoMapper.Profile;


namespace Ireview.Web.Models.Articles
{
    public class ArticleFeedViewModel : IMapTo<User>
    {
        public static int AmountArticles { get; set; } = 5;
        public List<Article> Articles { get; set; } 

        public void Mapping(MapProfile profile)
        {
            profile.CreateMap<User, ArticleFeedViewModel>();
        }
    }
}
