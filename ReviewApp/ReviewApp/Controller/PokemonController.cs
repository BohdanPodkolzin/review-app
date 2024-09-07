using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewApp.Dto;
using ReviewApp.Interfaces;
using ReviewApp.Models;

namespace ReviewApp.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController(IPokemonRepository pokemonRepository, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemons = mapper.Map<List<PokemonDto>>(pokemonRepository.GetPokemons());

            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            return Ok(pokemons);
        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof(PokemonDto))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int pokeId)
        {
            if (!pokemonRepository.PokemonExists(pokeId)) return NotFound();
            
            var pokemon = mapper.Map<PokemonDto>(pokemonRepository.GetPokemon(pokeId));
            
            if (!ModelState.IsValid) return BadRequest();

            return Ok(pokemon);
        }

        [HttpGet("{pokeId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonRating(int pokeId)
        {
            if (!pokemonRepository.PokemonExists(pokeId)) return NotFound();

            var pokeRating = pokemonRepository.GetPokemonRating(pokeId);

            if (!ModelState.IsValid) return BadRequest();
            
            return Ok(pokeRating);
        }
    }
}