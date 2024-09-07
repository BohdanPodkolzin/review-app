using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using ReviewApp.Dto;
using ReviewApp.Models;
using ReviewApp.Service;

namespace ReviewApp.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController(IOwnerRepository ownerRepository, IMapper mapper) : ControllerBase
    {
        private readonly IOwnerRepository _ownerRepository = ownerRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerDto>))]
        public IActionResult GetOwners()
        {
            var owners = _mapper.Map<ICollection<OwnerDto>>(_ownerRepository.GetOwners());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(owners);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200, Type = typeof(OwnerDto))]
        [ProducesResponseType(404)]
        public IActionResult GetOwner(int id)
        {
            if (!_ownerRepository.IsOwnerExists(id)) return NotFound();

            var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetOwner(id));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(owner);
        }

        [HttpGet("{ownerId}/pokemons")]
        [ProducesResponseType(200, Type = typeof(ICollection<PokemonDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetPokemonsByOwner(int ownerId)
        {
            if (!_ownerRepository.IsOwnerExists(ownerId)) return NotFound();

            var owners = _mapper.Map<ICollection<PokemonDto>>(_ownerRepository.GetPokemonsByOwner(ownerId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(owners);
        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<OwnerDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetOwnersByPokemon(int pokeId)
        {
            var pokemons = _mapper.Map<ICollection<OwnerDto>>(_ownerRepository.GetOwnersOfPokemon(pokeId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(pokemons);
        }

    }
}
