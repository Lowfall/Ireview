using Ireview.Infrastructure.Identity.Models;
using Ireview.Web.Models.Profile;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ireview.Infrastructure.Contexts;
using AutoMapper;
using Ireview.Core.Model;
using Ireview.Infrastructure.Contexts.Repositories;
using Ireview.Web.Interfaces;

namespace Ireview.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext db;
        private GenericRepository<Tag> tagRepository;
        private GenericRepository<User> userRepository;
        private GenericRepository<Article> articleRepository;
        private UserManager<AppUser> userManager;
        private readonly IMapper mapper;
        private IImageService imageService;
        public AccountController(AppDbContext db, IMapper mapper, UserManager<AppUser> userManager,IImageService imageService)
        {
            this.db = db;
            this.mapper = mapper;
            tagRepository = new GenericRepository<Tag>(db);
            userRepository = new GenericRepository<User>(db);
            articleRepository = new GenericRepository<Article>(db);
            this.userManager = userManager;
            this.imageService = imageService;
        }
        
        [Authorize]
        public IActionResult Profile(string id)
        {
            var currentUser = mapper.Map<UserProfileViewModel>(userRepository.Find(id));
            currentUser.Articles = articleRepository;
            return View(currentUser);
        }

        [Authorize]
        public IActionResult ArticleCreation()
        {
            return View();  
        }

        [HttpPost]
        public IActionResult PostArticle(Article article, IFormFile uploadedFile)
        {

            var currentUser = GetCurrentUser();
            var result = imageService.AddPhotoResult(uploadedFile);
            article.ImageSource = result.Result.SecureUrl.AbsoluteUri;
            article.Date = DateTime.Now;
            article.User = currentUser;
            article.Tag = new List<Tag>();
            article.Stars = 0;
            article.Rating = 0;
            article.User = currentUser;
            articleRepository.Create(article);
            return RedirectToAction("Profile",new { id = currentUser.Id });
        }

        public User GetCurrentUser()
        {
            return userRepository.Find(userManager.GetUserId(User));
        }
    }
}
