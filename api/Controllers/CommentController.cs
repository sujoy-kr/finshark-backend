using api.Dtos.Comment;
using api.Dtos.Stock.Comment;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("/api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Comment> comments = await _commentRepo.GetAllAsync();
            List<CommentDto> commentDtos = comments.Select(c => c.ToCommentDto()).ToList();
            return Ok(commentDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Comment? comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentRequestDto commentRequestDto)
        {
            bool stockExists = await _stockRepo.ExistsAsync(stockId);

            if (!stockExists)
            {
                return BadRequest("No Stock found associated with this Id.");
            }

            Comment commentModel = commentRequestDto.ToCommentFromCreate(stockId);

            await _commentRepo.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateCommentRequestDto updateCommentRequestDto)
        {
            Comment commentModel = updateCommentRequestDto.ToCommentFromUpdate();
            Comment? updatedComment = await _commentRepo.UpdateAsync(id, commentModel);
            if (updatedComment == null)
            {
                return NotFound("No Comment found associated with this Id.");
            }

            return Ok(updatedComment.ToCommentDto());
        }
    }
}