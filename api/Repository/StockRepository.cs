using api.Data;
using api.Dtos.Stock;
using api.Dtos.StockDto;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<StockDto>> GetAllAsync(QueryObject query)
        {
            var stocks = _context.Stocks
                .Include(x => x.Comments)
                .ThenInclude(a => a.AppUser)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol)
                        : stocks.OrderBy(s => s.Symbol);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks
                .Skip(skipNumber)
                .Take(query.PageSize)
                .Select(s => s.ToStockDto())
                .ToListAsync();
        }
        public async Task<StockDto?> GetByIdAsync(int id)
            => await _context.Stocks
                .Include(x => x.Comments)
                .Where(x => x.Id == id)
                .Select(s => s.ToStockDto())
                .FirstOrDefaultAsync();

        public async Task<StockDto?> GetBySymbolAsync(string symbol)
            => await _context.Stocks
                .Where(s => s.Symbol == symbol)
                .Select(s => s.ToStockDto())
                .FirstOrDefaultAsync();

    public async Task<StockDto> CreateAsync(CreateStockRequestDto stockDto)
    {
        var stockModel = stockDto.ToStockFromCreateDTO();
        await _context.Stocks.AddAsync(stockModel);
        await _context.SaveChangesAsync();

        return stockModel.ToStockDto();
    }

    public async Task<Tuple<int, UpdateStockRequestDto?>> UpdateAsync(int id, UpdateStockRequestDto updateDto)
    {
        var result = await _context.Stocks
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(x => x
            .SetProperty(s => s.Symbol, updateDto.Symbol)
            .SetProperty(s => s.CompanyName, updateDto.CompanyName)
            .SetProperty(s => s.Industry, updateDto.Industry)
            .SetProperty(s => s.Purchase, updateDto.Purchase)
            .SetProperty(s => s.LastDiv, updateDto.LastDiv)
            .SetProperty(s => s.MarketCap, updateDto.MarketCap));

        return new(result, updateDto);
    }

    public async Task<int> DeleteAsync(int id)
        => await _context.Stocks
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

    public async Task<bool> StockExists(int id)
        => await _context.Stocks.AnyAsync(x => x.Id == id);
}
}