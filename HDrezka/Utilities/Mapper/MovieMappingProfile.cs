using AutoMapper;
using HDrezka.Models;
using HDrezka.Models.DTOs;

namespace HDrezka.Utilities.Mapper
{
    public class MovieMappingProfile : Profile
    {
        public MovieMappingProfile() 
        {
            CreateMap<MovieDetailsDto, Movie>();
            CreateMap<Movie, MovieDetailsDto>();

            CreateMap<MovieDto, Movie>();
            CreateMap<Movie, MovieDto>();
        }
    }
}