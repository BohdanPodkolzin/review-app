using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewApp.Dto;
using ReviewApp.Models;
using ReviewApp.Repository;
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromBody] ReviewDto? reviewCreate)
        {
            if (reviewCreate == null) return BadRequest(ModelState);

            var isReviewExists = _reviewRepository
                .GetReviews()
                .FirstOrDefault(review => review.Title.Trim().ToUpper() == reviewCreate.Title.Trim().ToUpper());

            if (isReviewExists != null)
            {
                ModelState.AddModelError("", "Review with this Id already exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var reviewMap = mapper.Map<Review>(reviewCreate);

            if (reviewRepository.CreateReview(reviewMap))
                return Ok("Successfully created");

            ModelState.AddModelError("", "Something went wrong");
            return StatusCode(500, ModelState);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview(ReviewDto? reviewUpdate)
        {
            if (reviewUpdate == null) return BadRequest(ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var reviewMap = mapper.Map<Review>(reviewUpdate);

            if (_reviewRepository.UpdateReview(reviewMap)) return Ok("Successfully updated");

            ModelState.AddModelError("", "Something went wrong while updating");
            return StatusCode(500, ModelState);
        }

        [HttpDelete("{reviewId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult ReviewDelete(int reviewId, ReviewDto? reviewDelete)
        {
            if (reviewDelete == null) return BadRequest(ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var reviewMap = mapper.Map<Review>(reviewDelete);

            if (_reviewRepository.DeleteReview(reviewMap)) return Ok("Successfully deleted");

            ModelState.AddModelError("", "Something went wrong while deleting");
            return StatusCode(500, ModelState);
        }
    }
}
