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
using Microsoft.Data.SqlClient;

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
        public IActionResult Profile(string id ,string sortOrder, string filter)
        {
            var currentUser = mapper.Map<UserProfileViewModel>(userRepository.Find(id));
            currentUser.Articles = articleRepository.dbSet.Where(x => x.User.Id == currentUser.Id).ToList();

            switch (filter)
            {
                case "games":
                    currentUser.Articles = currentUser.Articles.Where(x=>x.Group == "Games").ToList();
                    break;
                case "books":
                    currentUser.Articles = currentUser.Articles.Where(x => x.Group == "Books").ToList();
                    break;
                case "music":
                    currentUser.Articles = currentUser.Articles.Where(x => x.Group == "Music").ToList();
                    break;
                case "films":
                    currentUser.Articles = currentUser.Articles.Where(x => x.Group == "Films").ToList();
                    break;
                default:
                    currentUser.Articles = currentUser.Articles.OrderBy(x => x.Title).ToList();
                    break;
            }

            switch (sortOrder)
            {
                case "date_desk":
                    currentUser.Articles = currentUser.Articles.OrderByDescending(x => x.Date).ToList();
                    break;
                case "date":
                    currentUser.Articles = currentUser.Articles.OrderBy(x => x.Date).ToList();
                    break;
                case "rate_desk":
                    currentUser.Articles = currentUser.Articles.OrderByDescending(x => x.Rating).ToList();
                    break;
                case "rate":
                    currentUser.Articles = currentUser.Articles.OrderBy(x => x.Rating).ToList();
                    break;
                default:
                    currentUser.Articles = currentUser.Articles.OrderBy(x => x.Title).ToList();
                    break;
            }
            return View(currentUser);
        }



        [Authorize]
        public IActionResult ArticleCreation()
        {
            return View();
        }
        
        [Authorize]
        public IActionResult EditPage(UserProfileViewModel user)
        {
            return View("EditPage", user);
        }

        [HttpPost]
        public IActionResult PostArticle(Article article, IFormFile uploadedFile)
        {

            var currentUser = GetCurrentUser();
            article.Date = DateTime.Now;
            article.User = currentUser;
            article.Tag = new List<Tag>();
            article.Rating = 0;
            article.User = currentUser;
            if (uploadedFile != null)
            {
                var result = imageService.AddPhotoResult(uploadedFile);
                article.ImageSource = result.Result.SecureUrl.AbsoluteUri;
                article.ImagePublicId = result.Result.PublicId;
            }
            else
            {
                article.ImageSource = "/resources/no_image.png";
                article.ImagePublicId = "";
            }
            articleRepository.Create(article);
            return RedirectToAction("Profile",new { id = currentUser.Id });
            return RedirectToAction("ArticleCreation");
        }

        [HttpPost]
        public IActionResult EditPage(UserProfileViewModel userProfile, IFormFile uploadedFile)
        {

            var currentUser = GetCurrentUser();
            if (userProfile.UserName == null)
            {
                return View("EditPage", userProfile);
            }
            currentUser.UserName = userProfile.UserName;
            currentUser.FirstName = userProfile.FirstName;
            currentUser.SecondName = userProfile.SecondName;
            currentUser.Gender = userProfile.Gender;
            if (uploadedFile != null)
            {
                var result = imageService.AddPhotoResult(uploadedFile);
                currentUser.ImageSource = result.Result.SecureUrl.AbsoluteUri;
                currentUser.ImagePublicId = result.Result.PublicId;
            }
            userRepository.Update(currentUser);
            return RedirectToAction("Profile", new { id = currentUser.Id });
        }

        public IActionResult SortByHighRating(string id)
        {
            var currentUser = mapper.Map<UserProfileViewModel>(userRepository.Find(id));
            return View("Profile",new{User = currentUser, id = currentUser.Id, sort = "high" });
        }
        public IActionResult SortByLowRating(string id)
        {
            var currentUser = mapper.Map<UserProfileViewModel>(userRepository.Find(id));
            currentUser.Articles = articleRepository.dbSet.Where(x => x.User.Id == currentUser.Id).ToList();
            currentUser.Articles.OrderBy(x => x.Rating);
            return View("Profile", currentUser);
        }
        public IActionResult SortByNewest(string id)
        {
            var currentUser = mapper.Map<UserProfileViewModel>(userRepository.Find(id));
            currentUser.Articles = articleRepository.dbSet.Where(x => x.User.Id == currentUser.Id).ToList();
            currentUser.Articles.OrderByDescending(x => x.Date);
            return View("Profile", currentUser);
        }
        public IActionResult SortByOlder(string id)
        {
            var currentUser = mapper.Map<UserProfileViewModel>(userRepository.Find(id));
            currentUser.Articles = articleRepository.dbSet.Where(x => x.User.Id == currentUser.Id).ToList();
            currentUser.Articles.OrderBy(x => x.Date);
            return View("Profile", currentUser);
        }


        public User GetCurrentUser()
        {
            return userRepository.Find(userManager.GetUserId(User));
        }
    }
}
