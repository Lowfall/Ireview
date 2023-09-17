using AutoMapper;
using Ireview.Core.Mapping;
using Ireview.Core.Model;
using Ireview.Infrastructure.Contexts.Repositories;
using Ireview.Infrastructure.Identity.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using MapProfile = AutoMapper.Profile;


namespace Ireview.Web.Models.Profile
{
    public class UserProfileViewModel:IMapTo<User>
    {
        public string Id { get; set; } 
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public DateTime? RegisterDate { get; set; }
        public GenericRepository<Article> Articles { get; set; }

        public void Mapping(MapProfile profile)
        {
            profile.CreateMap<User, UserProfileViewModel>();
        }
    }
}
