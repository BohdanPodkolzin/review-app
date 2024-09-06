﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var categories = mapper.Map<ICollection<CategoryDto>>(_categoryRepository.GetCategories());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(404)]
        public IActionResult GetCategory(int id)
        {
            if (!_categoryRepository.IsCategoryExists(id)) return NotFound();

            var category = mapper.Map<CategoryDto>(_categoryRepository.GetCategory(id));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(category);
        }

        [HttpGet("{categoryId}/pokemons")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(404)]
        public IActionResult GetPokemonByCategory(int categoryId)
        {
            if (!_categoryRepository.IsCategoryExists(categoryId)) return NotFound();

            var pokemons = mapper.Map<List<PokemonDto>>(_categoryRepository.GetPokemonByCategory(categoryId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(pokemons);
        }
    }
}
