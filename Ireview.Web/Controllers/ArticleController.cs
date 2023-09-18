using AutoMapper;
using Ireview.Core.Model;
using Ireview.Infrastructure.Contexts;
using Ireview.Infrastructure.Contexts.Repositories;
using Ireview.Infrastructure.Identity.Models;
using Ireview.Web.Interfaces;
using Ireview.Web.Models.Articles;
using Ireview.Web.Models.Profile;
using Ireview.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ireview.Web.Controllers
{
    public class ArticleController : Controller
    {
        private GenericRepository<Article> articleRepository;
        private GenericRepository<User> userRepository;
        private readonly IMapper mapper;
        private UserManager<AppUser> userManager;
        private IImageService imageService;
        public ArticleController(AppDbContext db, IMapper mapper, UserManager<AppUser> userManager, IImageService imageService)
        {
            articleRepository = new GenericRepository<Article>(db);
            userRepository = new GenericRepository<User>(db);
            this.mapper = mapper;
            this.userManager = userManager;
            this.imageService = imageService;
        }
        public IActionResult ArticlePage (int id)
        {
            var currentArticle = mapper.Map<ArticlePageViewModel>(articleRepository.dbSet.Find(id));
            currentArticle.Users = userRepository;
            return View(currentArticle);
        }

        public IActionResult EditArticle(Article article)
        {
            return View("ArticleEditing", article);
        }

        [HttpPost]
        public IActionResult EditArticle(Article article, IFormFile uploadedFile)
        {
            var currentUser = GetCurrentUser();
            article.User = currentUser;
            article.Date = DateTime.Now;
            if(uploadedFile != null)
            {
                var result = imageService.AddPhotoResult(uploadedFile);
                article.ImageSource = result.Result.SecureUrl.AbsoluteUri;
                article.ImagePublicId = result.Result.PublicId;
            }
            if (article.ImagePublicId == null)
            {
                article.ImagePublicId = "";
            }
            articleRepository.Update(article);
            return RedirectToAction("Profile","Account", new { id = currentUser.Id });
        }

        public IActionResult DeleteArticle(int id)
        {
            var article = articleRepository.dbSet.Find(id);
            imageService.DeletePhotoResult(article.ImagePublicId);
            articleRepository.Delete(article);
            return RedirectToAction("Profile", "Account", new { id = GetCurrentUser().Id });
        }

        public User GetCurrentUser()
        {
            return userRepository.Find(userManager.GetUserId(User));
        }
    }
}
