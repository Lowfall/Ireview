using Ireview.Core.Model;
using Ireview.Infrastructure.Contexts;
using Ireview.Infrastructure.Contexts.Repositories;

namespace Ireview.Web.Models.Home
{
    public class MainViewModel
    {
        public GenericRepository<Article> Articles { get; set; }
        public GenericRepository<User> Users { get; set; }
    }
}
