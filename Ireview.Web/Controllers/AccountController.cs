using Ireview.Infrastructure.Identity.Models;
using Ireview.Web.Models.Profile;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ireview.Infrastructure.Contexts;
using AutoMapper;

namespace Ireview.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;
        public AccountController(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        
        [Authorize]
        public IActionResult Profile(string id)
        {
            var user = db.Users.Find(id);
            var x = mapper.Map<UserProfileViewModel>(user);
            return View(x);
        }

        [Authorize]
        public IActionResult ArticleCreation()
        {
            return View();  
        }
    }
}
