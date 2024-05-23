using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockRepository _stockRepository;
        public CommentRepository(
            ApplicationDbContext context,
            IStockRepository stockRepository)
        {
            _context = context;
            _stockRepository = stockRepository;
        }

        public async Task<List<CommentDto>> GetAllAsync()
        {
            return await _context.Comments
                .Select(x => x.ToCommentDto())
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<CommentDto?> GetByIdAsync(int id)
        {
            return await _context.Comments
                .Where(x => x.Id == id)
                .Select(x => x.ToCommentDto())
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<Tuple<int, CommentDto>?> CreateAsync(int stockId, CreateCommentDto createModel)
        { 
            var comment = createModel.ToCommentFromCreateDto(stockId);
            await _context.Comments.AddAsync(comment);
            var result = await _context.SaveChangesAsync();

            return new(result, comment.ToCommentDto());
        }

        public async Task<Tuple<int, UpdateCommentDto>> UpdateAsync(int id, UpdateCommentDto updateDto)
        {
            var result = await _context.Comments
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(x => x
                .SetProperty(c => c.Title, updateDto.Title)
                .SetProperty(c => c.Content, updateDto.Content));

            return new(result, updateDto);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var result = await _context.Comments
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            return result;
        }
    }
}