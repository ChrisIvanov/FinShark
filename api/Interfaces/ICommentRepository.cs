using api.Dtos.Comment;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<CommentDto>> GetAllAsync();

        Task<CommentDto?> GetByIdAsync(int id);

        Task<Tuple<int, CommentDto>?> CreateAsync(int stockId, CreateCommentDto createModel);

        Task<Tuple<int, UpdateCommentDto>> UpdateAsync(int id, UpdateCommentDto updateDto);

        Task<int> DeleteAsync(int id);
    }
}