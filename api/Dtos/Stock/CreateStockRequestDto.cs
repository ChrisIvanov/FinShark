using System.ComponentModel.DataAnnotations;
using Azure.Core;

namespace api.Dtos.Stock
{
    public class CreateStockRequestDto
    {
        [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 characters")]
        public required string Symbol { get; set; } = string.Empty;

        [MaxLength(10, ErrorMessage = "Company cannot be over 10 characters")]
        public required string CompanyName { get; set; } = string.Empty;

        [Range(1, 1000000000)]
        public required decimal Purchase { get; set; }

        [Range(0.001, 100)]
        public required decimal LastDiv { get; set; }

        [MaxLength(10, ErrorMessage = "Industry cannot be over 10 characters")]
        public required string Industry { get; set; } = string.Empty;

        [Range(1, 1000000000)]
        public required long MarketCap { get; set; }
    }
}