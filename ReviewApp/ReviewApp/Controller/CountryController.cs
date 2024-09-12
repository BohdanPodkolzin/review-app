using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewApp.Dto;
using ReviewApp.Interfaces;
using ReviewApp.Models;
using ReviewApp.Service;

namespace ReviewApp.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController(ICountryRepository countryRepository, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CountryDto>))]
        public IActionResult GetCountries()
        {
            var countries = mapper.Map<ICollection<CountryDto>>(countryRepository.GetCountries());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(countries);
        }

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(CountryDto))]
        [ProducesResponseType(404)]
        public IActionResult GetCountry(int countryId)
        {
            if (!countryRepository.IsCountryExists(countryId)) return NotFound();

            var country = mapper.Map<CountryDto>(countryRepository.GetCountry(countryId)); 

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(country);
        }

        [HttpGet("{countryId}/owners")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetOwnersByCountry(int countryId)
        {
            if (!countryRepository.IsCountryExists(countryId)) return NotFound();

              
            var owners = mapper.Map<List<OwnerDto>>(countryRepository.GetOwnersByCountry(countryId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(owners);
        }

        [HttpGet("{ownerId}/country")]
        [ProducesResponseType(200, Type = typeof(CountryDto))]
        [ProducesResponseType(404)]
        public IActionResult Get(int ownerId)
        {
            var country = mapper.Map<CountryDto>(countryRepository.GetCountryByOwner(ownerId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(country);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult CreateCountry([FromBody] CountryDto? countryCreate)
        {
            if (countryCreate == null) return BadRequest(ModelState);

            var isCountryExists = countryRepository
                .GetCountries()
                .FirstOrDefault(country => country.Name.Trim().ToUpper() == countryCreate.Name.Trim().ToUpper());

            if (isCountryExists != null)
            {
                ModelState.AddModelError("", "Country already exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var countryMap = mapper.Map<Country>(countryCreate);

            if (countryRepository.CreateCountry(countryMap))
                return Ok("Successfully created");

            ModelState.AddModelError("", "Something went wrong");
            return StatusCode(500, ModelState);
        }
    }
}
