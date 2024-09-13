using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewApp.Dto;
using ReviewApp.Models;
using ReviewApp.Service;

namespace ReviewApp.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController(ICategoryRepository categoryRepository, IMapper mapper) : ControllerBase
    {
        public readonly ICategoryRepository _categoryRepository = categoryRepository;
        public readonly IMapper _mapper = mapper;

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public IActionResult GetCategories()
        {
            var categories = mapper.Map<ICollection<CategoryDto>>(_categoryRepository.GetCategories());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        [ProducesResponseType(404)]
        public IActionResult GetCategory(int id)
        {
            if (!_categoryRepository.IsCategoryExists(id)) return NotFound();

            var category = mapper.Map<CategoryDto>(_categoryRepository.GetCategory(id));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(category);
        }

        [HttpGet("{categoryId}/pokemons")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PokemonDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetPokemonByCategory(int categoryId)
        {
            if (!_categoryRepository.IsCategoryExists(categoryId)) return NotFound();

            var pokemons = mapper.Map<List<PokemonDto>>(_categoryRepository.GetPokemonByCategory(categoryId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(pokemons);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromQuery] int pokemonId, [FromBody] CategoryDto? categoryCreate)
        {
            if (categoryCreate == null) return BadRequest(ModelState);

            var categories = _categoryRepository
                .GetCategories()
                .FirstOrDefault(category => category.Name.Trim().ToUpper() == categoryCreate.Name.Trim().ToUpper());

            if (categories != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var categoryMap = mapper.Map<Category>(categoryCreate);

            if (_categoryRepository.CreateCategory(pokemonId, categoryMap))
                return Ok("Successfully created category!");
            
            ModelState.AddModelError("", "Something went wrong");
            return StatusCode(500, ModelState);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory([FromBody]CategoryDto? categoryUpdate)
        {
            if (categoryUpdate == null) return BadRequest(ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var categoryMap = mapper.Map<Category>(categoryUpdate);

            if (_categoryRepository.UpdateCategory(categoryMap)) return Ok("Successfully updated!");

            ModelState.AddModelError("", "Something went wrong when updating");
            return StatusCode(500, ModelState);
        }

        [HttpDelete("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult CategoryDelete(int categoryId, CategoryDto? categoryDelete)
        {
            if (categoryDelete == null) return BadRequest(ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var categoryMap = mapper.Map<Category>(categoryDelete);

            if (_categoryRepository.DeleteCategory(categoryMap)) return Ok("Successfully deleted");

            ModelState.AddModelError("", "Something went wrong while deleting");
            return StatusCode(500, ModelState);
        }
    }
}
