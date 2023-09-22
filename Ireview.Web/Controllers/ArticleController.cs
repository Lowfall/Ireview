using AutoMapper;
using Ireview.Core.Model;
using Ireview.Infrastructure.Contexts;
using Ireview.Infrastructure.Contexts.Repositories;
using Ireview.Infrastructure.Identity.Models;

using Ireview.Web.Interfaces;
using Ireview.Web.Models.Articles;
using Ireview.Web.Models.Profile;
using Ireview.Web.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using UserManagerCore = Microsoft.AspNetCore.Identity.UserManager<Ireview.Infrastructure.Identity.Models.AppUser>;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using CloudinaryDotNet;

namespace Ireview.Web.Controllers
{
    public class ArticleController : Controller
    {
        private GenericRepository<Article> articleRepository;
        private GenericRepository<User> userRepository;
        private GenericRepository<Stars> starRepository;
        private readonly IMapper mapper;
        private UserManagerCore userManager;
        private IImageService imageService;
        public ArticleController(AppDbContext db, IMapper mapper, UserManagerCore userManager, IImageService imageService)
        {
            articleRepository = new GenericRepository<Article>(db);
            userRepository = new GenericRepository<User>(db);
            starRepository = new GenericRepository<Stars>(db);
            this.mapper = mapper;
            this.userManager = userManager;
            this.imageService = imageService;
        }
        public IActionResult ArticlePage (int id)
        {
            var currentArticle = mapper.Map<ArticlePageViewModel>(articleRepository.dbSet.Find(id));
            currentArticle.Users = userRepository;
            var user = GetCurrentUser();
            if (user != null)
            {
                GetArticleStars(user.Id, currentArticle.Id, out var articleStars);
                if(articleStars != null)
                {
                    currentArticle.StarsAmount = articleStars.Amount;
                }
            }
            return View(currentArticle);
        }

        public IActionResult EditArticle(Article article)
        {
            return View("ArticleEditing", article);
        }

        [Authorize]
        [HttpPost]
        public IActionResult SetRating(int id, int stars)
        {
            var article = articleRepository.dbSet.Find(id);
            var user = GetCurrentUser();
            
            if (GetArticleStars(user.Id,article.Id,out var articleStars))
            {
                articleStars.Amount = stars;
                starRepository.Update(articleStars);
            }
            else
            {
                Stars star = new Stars() { ArticleId = article.Id, Article = article, Amount = stars, User = user, UserId = user.Id };
                starRepository.Create(star);
            }

            article.Rating = CountAverageRating(id);
            articleRepository.Update(article);
            return RedirectToAction("ArticlePage","Article", new { id = id });
        }

        [HttpPost]
        public IActionResult EditArticle(Article article, IFormFile uploadedFile)
        {
            var currentUser = GetCurrentUser();
            if (article.Header != null && article.Title != null && article.Text != null)
            {
                article.User = currentUser;
                article.Date = DateTime.Now;
                if (uploadedFile != null)
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
                return RedirectToAction("Profile", "Account", new { id = currentUser.Id });
            }
            return View("ArticleEditing", article);
        }

        public IActionResult ArticleFeed()
        {
            var articleList = articleRepository.dbSet.Take(ArticleFeedViewModel.AmountArticles).ToList();
            if (ArticleFeedViewModel.AmountArticles < articleRepository.dbSet.Count())
            {
                ArticleFeedViewModel.AmountArticles += 5;
            }
            return View("ArticlesFeed", new ArticleFeedViewModel() { Articles = articleList });
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

        public float CountAverageRating (int id)
        {
            var sum = starRepository.dbSet.Where(x => x.Article.Id == id).Select(x => x.Amount).Sum();
            var amount = starRepository.dbSet.Where(x => x.Article.Id == id).Count();
            return sum / amount;
        }

        public bool GetArticleStars(string userId, int articleId, out Stars stars)
        {
            if (starRepository.dbSet.Where(x => x.UserId == userId && x.ArticleId == articleId).Count() != 0)
            {
                stars = starRepository.dbSet.Where(x => x.UserId == userId && x.ArticleId == articleId).Single();
                return true;
            }
            else
            {
                stars = null;
                return false;
            }
        }
    }
}
