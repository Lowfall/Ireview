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
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Localization;
using Azure.Core;
using Microsoft.AspNetCore.Localization;

namespace Ireview.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHtmlLocalizer<HomeController> localizer;
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext db;
        private readonly GenericRepository<Article> articleRepository;
        private readonly GenericRepository<User> userRepository;

        public HomeController(ILogger<HomeController> logger, AppDbContext db, IHtmlLocalizer<HomeController> localizer)
        {
            _logger = logger;
            this.db = db;
            articleRepository = new GenericRepository<Article>(db);
            userRepository = new GenericRepository<User>(db);
            this.localizer = localizer;
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

        public IActionResult SetENLanguage()
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("en")),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return Redirect(Request.Headers["Referer"].ToString() ?? "/");
        }

        public IActionResult SetRULanguage()
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("ru")),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return Redirect(Request.Headers["Referer"].ToString() ?? "/");
        }
    }
}