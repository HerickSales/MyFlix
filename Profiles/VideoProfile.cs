using AutoMapper;
using MyFlix.Data.Dtos;
using MyFlix.Models;

namespace MyFlix.Profiles
{
    public class VideoProfile : Profile
    {
        public VideoProfile()
        {
            CreateMap<CreateVideoDto, Videos>();
            CreateMap<UpdateVideoDto, Videos>();
            CreateMap<Videos,UpdateVideoDto>();
            CreateMap<Videos, ReadVideoDto>();
        }
    }
}
