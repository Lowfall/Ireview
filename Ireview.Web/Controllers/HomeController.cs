using Ireview.Infrastructure.Identity.Models;
using Ireview.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Ireview.Infrastructure.Contexts;
using Ireview.Infrastructure.Contexts.Repositories;
using Ireview.Core.Model;
using Ireview.Web.Models.Home;

namespace Ireview.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext db;
        private readonly GenericRepository<Article> articleRepository;
        private readonly GenericRepository<User> userRepository;

        public HomeController(ILogger<HomeController> logger, AppDbContext db)
        {
            _logger = logger;
            this.db = db;
            articleRepository = new GenericRepository<Article>(db);
            userRepository = new GenericRepository<User>(db);
        }

        public IActionResult Index()
        {
            return View(new MainViewModel() { Articles = articleRepository, Users = userRepository });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}