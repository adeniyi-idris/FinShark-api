using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Mapper
{
    public static class StockMapper
    {
        public static StockDto ToStockDto(this stock stockModel)// returning stockdto coming from stock in models
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }

        public static stock ToStockFromCreateDto(this CreateStockRequestDto StockDto)// returning stock coming from createDto
        {
            return new stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };
        }
        
    }
}