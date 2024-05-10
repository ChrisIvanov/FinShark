using api.Dtos.Comment;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;


        public CommentController(
            ICommentRepository commentRepository,
            IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _commentRepository.GetAllAsync();
            return Ok(comments);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepository.GetByIdAsync(id);

            if (comment == null)
                return NotFound();

            return Ok(comment);
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto createModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _stockRepository.StockExists(stockId))
                return BadRequest("Stock doesn't exist.");

            var result = await _commentRepository.CreateAsync(stockId, createModel);

            if (result == null)
                return NotFound();

            return CreatedAtAction(nameof(GetById), new { id = result.Item2.Id }, result.Item2);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto updateModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _commentRepository.UpdateAsync(id, updateModel);

            if (result.Item1 == 0)
                return NotFound();

            return Ok(result.Item2);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var result = await _commentRepository.DeleteAsync(id);

            if (result == 0)
                return NotFound();

            return NoContent();
        }
    }
}