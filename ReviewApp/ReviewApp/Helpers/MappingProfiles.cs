﻿using AutoMapper;
using ReviewApp.Dto;
using ReviewApp.Models;

namespace ReviewApp.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Country, CountryDto>();
        }
    }
}
