using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult PostPokemon([FromQuery] int ownerId, [FromQuery] int categoryId, [FromBody] PokemonDto? pokemonCreate)
        {
            if (pokemonCreate == null) return BadRequest(ModelState);

            var pokemons = pokemonRepository
                .GetPokemons()
                .FirstOrDefault(pokemons => pokemons.Name.Trim().ToUpper() == pokemonCreate.Name.Trim().ToUpper());

            if (pokemons != null)
            {
                ModelState.AddModelError("", "Pokemon already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var pokemonMap = mapper.Map<Pokemon>(pokemonCreate);

            if (pokemonRepository.CreatePokemon(ownerId, categoryId, pokemonMap))
                return Ok("Successfully created!");

            ModelState.AddModelError("", "Something went wrong");
            return StatusCode(500, ModelState);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePokemon([FromBody] PokemonDto? pokemonUpdate)
        {
            if (pokemonUpdate == null) return BadRequest(ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var pokemonMap = mapper.Map<Pokemon>(pokemonUpdate);

            if (pokemonRepository.UpdatePokemon(pokemonMap)) return Ok("Successfully updated!");

            ModelState.AddModelError("", "Something went wrong when updating");
            return StatusCode(500, ModelState);
        }
    }
}