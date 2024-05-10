using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Dtos.StockDto;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto()
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Industry = stockModel.Industry,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(x => x.ToCommentDto()).ToList()
            };
        }

        public static Stock ToStockFromCreateDTO(this CreateStockRequestDto stockDto)
        {
            return new Stock()
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Industry = stockDto.Industry,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                MarketCap = stockDto.MarketCap,
                Comments = new List<Comment>()
            };
        }
    }
}