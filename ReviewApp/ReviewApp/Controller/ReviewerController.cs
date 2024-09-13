using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewApp.Dto;
using ReviewApp.Models;
using ReviewApp.Service;

namespace ReviewApp.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper) : ControllerBase
    {
        private readonly IReviewerRepository _reviewerRepository = reviewerRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerDto>))]
        public IActionResult GetReviewers()
        {
            var reviews = _mapper.Map<ICollection<ReviewerDto>>(_reviewerRepository.GetReviewers());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetReviewer(int id)
        {
            if (!_reviewerRepository.IsReviewerExists(id)) return NotFound();

            var reviewer = _mapper.Map<ReviewerDto>(_reviewerRepository.GetReviewer(id));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(reviewer);
        }

        [HttpGet("{reviewerId}/reviews")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            if (!_reviewerRepository.IsReviewerExists(reviewerId)) return NotFound();

            var reviews = _mapper.Map<ICollection<ReviewDto>>(_reviewerRepository.GetReviewsByReviewer(reviewerId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReviewer(ReviewerDto? reviewerUpdate)
        {
            if (reviewerUpdate == null) return BadRequest(ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var reviewerMap = mapper.Map<Reviewer>(reviewerUpdate);

            if (_reviewerRepository.UpdateReviewer(reviewerMap)) return Ok("Successfully updated");

            ModelState.AddModelError("", "Something went wrong while updating");
            return StatusCode(500, ModelState);
        }

        [HttpDelete("{reviewerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReviewer(int reviewerId, ReviewerDto? reviewerDelete)
        {
            if (reviewerDelete == null) return BadRequest(ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var reviewerMap = mapper.Map<Reviewer>(reviewerDelete);

            if (_reviewerRepository.DeleteReview(reviewerMap)) return Ok("Successfully deleted");

            ModelState.AddModelError("", "Something went wrong while deleting");
            return StatusCode(500, ModelState);
        }

    }
}
