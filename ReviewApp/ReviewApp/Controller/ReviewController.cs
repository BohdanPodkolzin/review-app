using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewApp.Dto;
using ReviewApp.Service;

namespace ReviewApp.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController(IReviewRepository reviewRepository, IMapper mapper) : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository = reviewRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        public IActionResult GetReviews()
        {
            var reviews = mapper.Map<ICollection<ReviewDto>>(_reviewRepository.GetReviews());

            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            return Ok(reviews);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200, Type = typeof(ReviewDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReview(int id)
        {
            if (!reviewRepository.IsReviewExists(id)) return NotFound();

            var review = mapper.Map<ReviewDto>(_reviewRepository.GetReview(id));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpGet("{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            var reviews = mapper.Map<ICollection<ReviewDto>>(_reviewRepository.GetReviewsByReviewer(reviewerId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsOfPokemon(int pokeId)
        {
            var reviews = mapper.Map<ICollection<ReviewDto>>(_reviewRepository.GetReviewsOfPokemon(pokeId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(reviews);
        }
    }
}
