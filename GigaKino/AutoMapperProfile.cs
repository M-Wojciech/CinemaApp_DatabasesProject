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

            CreateMap<Kino, KinoDTO>().ReverseMap();
            CreateMap<KinoDTO, Kino>();

            CreateMap<Sala, SalaDTO>().ReverseMap();
            CreateMap<SalaDTO, Sala>();

            CreateMap<Miejsce, MiejsceDTO>().ReverseMap();
            CreateMap<MiejsceDTO, Miejsce>();

            CreateMap<Seans, SeansDTO>().ReverseMap();
            CreateMap<SeansDTO, Seans>();

            CreateMap<Bilet, BiletDTO>().ReverseMap();
            CreateMap<BiletDTO, Bilet>();

            CreateMap<Transakcja, TransakcjaDTO>().ReverseMap();
            CreateMap<TransakcjaDTO, Transakcja>();

            CreateMap<Klient, KlientDTO>().ReverseMap();
            CreateMap<KlientDTO, Klient>();

            CreateMap<Konto, KontoDTO>().ReverseMap();
            CreateMap<KontoDTO, Konto>();

            CreateMap<Pracownik, PracownikDTO>().ReverseMap();
            CreateMap<PracownikDTO, Pracownik>();
        }
    }
}
