using AutoMapper;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.DTOs;

public class PhotoDTO : IMapFrom<Photo>
{
    public int Id { get; set; }
    public string Path { get; set; } = String.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Photo, PhotoDTO>()
            .ForMember(x=>x.Path, opt=>opt.MapFrom(x=>x.ShortPath));
    }
}
