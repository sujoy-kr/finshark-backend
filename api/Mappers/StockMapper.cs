using api.Dtos.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMapper
    {
        public static StockDto ToStockDto(this Stock stockModel)
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
            };
        }

        public static Stock ToStockFormDto(this CreateStockRequestDto stockFormDto)
        {
            return new Stock
            {
                Symbol = stockFormDto.Symbol,
                CompanyName = stockFormDto.CompanyName,
                Purchase = stockFormDto.Purchase,
                LastDiv = stockFormDto.LastDiv,
                Industry = stockFormDto.Industry,
                MarketCap = stockFormDto.MarketCap,
            };
        }
    }
}