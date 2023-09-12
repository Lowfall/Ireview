using AutoMapper;
using Ireview.Core.Mapping;
using Ireview.Infrastructure.Identity.Models;
using System.ComponentModel.DataAnnotations;
using MapProfile = AutoMapper.Profile;


namespace Ireview.Web.Models.Profile
{
    public class UserProfileViewModel:IMapTo<AppUser>
    {
        public int Id { get; set; } 
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? RegisterDate { get; set; }

        public void Mapping(MapProfile profile)
        {
            profile.CreateMap<AppUser, UserProfileViewModel>().ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
