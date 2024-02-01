using AutoMapper;
using MyFlix.Data.Dtos;
using MyFlix.Models;

namespace MyFlix.Profiles
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<CreateCategoriaDto, Categoria>();
            CreateMap<UpdateCategoriaDto, Categoria>();
            CreateMap<Categoria, UpdateCategoriaDto>();
            CreateMap<Categoria, ReadCategoriaDto>()
                .ForMember(categoriaDto => categoriaDto.Videos,
                    opt => opt.MapFrom(categoria => categoria.Videos));
        }

    }
}
