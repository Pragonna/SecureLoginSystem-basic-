using AutoMapper;
using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Users.Features.Commands.ModifyCommand;
using Core.Infrastructure.Persistence.Users.Features.Dtos;

namespace Core.Infrastructure.Persistence.Users.Features.Profiles
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {

            // UserModifyCommand -> UserDTO mapping
            CreateMap<UserModifyCommand, User>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.image != null ? AdaptionImage(src) : null));

            CreateMap<User, UserDto>().ReverseMap();
        }
        private Image AdaptionImage(UserModifyCommand src)
        {
            using var ms = new MemoryStream();
            src.image.CopyTo(ms);
            var fileBytes = ms.ToArray();
            return new Image
            {
                FileName = src.image.FileName,
                ContentType = src.image.ContentType,
                ContentData = fileBytes,
                CapacityMB = fileBytes.Length / 1024 / 1024,
                Length = fileBytes.Length
            };
        }
    }
}
