using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview(int pokemonId, OwnerDto? ownerCreate)
        {
            if (ownerCreate == null) return BadRequest(ModelState);

            var IsOwnerExists = _ownerRepository
                .GetOwners()
                .FirstOrDefault(owner => owner.FirstName.Trim().ToUpper() == ownerCreate.FirstName.Trim().ToUpper()
                                                     && owner.LastName.Trim().ToUpper() == owner.LastName.Trim().ToUpper());

            
            if (IsOwnerExists != null)
            {
                ModelState.AddModelError("", "Owner with this name already exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var ownerMap = mapper.Map<Owner>(ownerCreate);

            if (_ownerRepository.CreateOwner(pokemonId, ownerMap))
                return Ok("Successfully created");

            ModelState.AddModelError("", "Something went wrong");
            return StatusCode(500, ModelState);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOwner([FromBody] OwnerDto? ownerUpdate)
        {
            if (ownerUpdate == null) return BadRequest(ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var ownerMap = mapper.Map<Owner>(ownerUpdate);

            if (_ownerRepository.UpdateOwner(ownerMap)) return Ok("Successfully updated!");

            ModelState.AddModelError("", "Something went wrong while updating");
            return StatusCode(500, ModelState);
        }

    }
}
