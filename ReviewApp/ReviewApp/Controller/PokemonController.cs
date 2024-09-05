using Microsoft.AspNetCore.Mvc;
using ReviewApp.Interfaces;
using ReviewApp.Models;

namespace ReviewApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController(IPokemonRepository pokemonRepository) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemons = pokemonRepository.GetPokemons();

            if (!ModelState.IsValid) return BadRequest(ModelState);
            

            return Ok(pokemons);
        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int pokeId)
        {
            if (!pokemonRepository.PokemonExists(pokeId)) return NotFound();
            
            var pokemon = pokemonRepository.GetPokemon(pokeId);
            
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