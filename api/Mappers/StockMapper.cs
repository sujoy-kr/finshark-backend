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
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }

        public static Stock ToStockFormCreate(this CreateStockRequestDto createStockRequestDto)
        {
            return new Stock
            {
                Symbol = createStockRequestDto.Symbol,
                CompanyName = createStockRequestDto.CompanyName,
                Purchase = createStockRequestDto.Purchase,
                LastDiv = createStockRequestDto.LastDiv,
                Industry = createStockRequestDto.Industry,
                MarketCap = createStockRequestDto.MarketCap,
            };
        }

        public static Stock ToStockFormUpdate(this UpdateStockRequestDto updateStockRequestDto)
        {
            return new Stock
            {
                Symbol = updateStockRequestDto.Symbol,
                CompanyName = updateStockRequestDto.CompanyName,
                Purchase = updateStockRequestDto.Purchase,
                LastDiv = updateStockRequestDto.LastDiv,
                Industry = updateStockRequestDto.Industry,
                MarketCap = updateStockRequestDto.MarketCap,
            };
        }

        public static UpdateStockResponseDto ToUpdateStockResponseDto(this Stock stockModel)
        {
            return new UpdateStockResponseDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
            };
        }
    }
}