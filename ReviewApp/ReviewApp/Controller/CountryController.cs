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
        public IActionResult GetCountry(int countryId)
        {
            if (!countryRepository.IsCountryExists(countryId)) return NotFound();

            var country = mapper.Map<CountryDto>(countryRepository.GetCountry(countryId)); 

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(country);
        }

        [HttpGet("{countryId}/owners")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        public IActionResult GetOwnersByCountry(int countryId)
        {
            if (!countryRepository.IsCountryExists(countryId)) return NotFound();

            //  
            var owners = countryRepository.GetOwnersByCountry(countryId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(owners);
        }

        [HttpGet("{ownerId}/country")]
        [ProducesResponseType(200, Type = typeof(Country))]
        public IActionResult Get(int ownerId)
        {
            var country = mapper.Map<CountryDto>(countryRepository.GetCountryByOwner(ownerId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(country);
        }

    }
}
