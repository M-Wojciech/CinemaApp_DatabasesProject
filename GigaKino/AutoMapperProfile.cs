using AutoMapper;
using GigaKino.Models;
using GigaKino.ObjectsDTO;

namespace GigaKino
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Film, FilmDTO>().ReverseMap(); // Map Film to FilmDTO
            CreateMap<FilmDTO, Film>(); // Map FilmDTO to Film
        }
    }
}
