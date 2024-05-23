using api.Dtos.Stock;
using api.Dtos.StockDto;
using api.Helpers;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<StockDto>> GetAllAsync(QueryObject query);

        Task<StockDto?> GetByIdAsync(int id);

        Task<StockDto?> GetBySymbolAsync(string symbol);


        Task<StockDto> CreateAsync(CreateStockRequestDto stockModel);

        Task<Tuple<int, UpdateStockRequestDto?>> UpdateAsync(int id, UpdateStockRequestDto stockDto);

        Task<int> DeleteAsync(int id);

        Task<bool> StockExists(int id);
    }
}