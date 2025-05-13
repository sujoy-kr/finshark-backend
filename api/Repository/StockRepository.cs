using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using api.Helpers;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            Stock? stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stockModel == null)
            {
                return null;
            }

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();

            return stockModel;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<List<Stock>> GetAllAsync(StockQueryObject query)
        {
            IQueryable<Stock> queryableStocks = _context.Stocks.Include(c => c.Comments).AsQueryable();
            List<Stock> stocks = new List<Stock>();

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                queryableStocks = queryableStocks.Where(s => s.Symbol.Contains(query.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                queryableStocks = queryableStocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    queryableStocks = query.IsDescending ? queryableStocks.OrderByDescending(s => s.Symbol) : queryableStocks.OrderBy(s => s.Symbol);
                }

                if (query.SortBy.Equals("CompanyName", StringComparison.OrdinalIgnoreCase))
                {
                    queryableStocks = query.IsDescending ? queryableStocks.OrderByDescending(s => s.CompanyName) : queryableStocks.OrderBy(s => s.CompanyName);
                }

                if (query.SortBy.Equals("Purchase", StringComparison.OrdinalIgnoreCase))
                {
                    queryableStocks = query.IsDescending ? queryableStocks.OrderByDescending(s => s.Purchase) : queryableStocks.OrderBy(s => s.Purchase);
                }

                if (query.SortBy.Equals("LastDiv", StringComparison.OrdinalIgnoreCase))
                {
                    queryableStocks = query.IsDescending ? queryableStocks.OrderByDescending(s => s.LastDiv) : queryableStocks.OrderBy(s => s.LastDiv);
                }

                if (query.SortBy.Equals("Industry", StringComparison.OrdinalIgnoreCase))
                {
                    queryableStocks = query.IsDescending ? queryableStocks.OrderByDescending(s => s.Industry) : queryableStocks.OrderBy(s => s.Industry);
                }

                if (query.SortBy.Equals("MarketCap", StringComparison.OrdinalIgnoreCase))
                {
                    queryableStocks = query.IsDescending ? queryableStocks.OrderByDescending(s => s.MarketCap) : queryableStocks.OrderBy(s => s.MarketCap);
                }
            }

            int skipNumber = (query.PageNumber - 1) * query.PageSize;

            stocks = await queryableStocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
            return stocks;
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            Stock? stock = await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(s => s.Id == id);
            return stock;
        }

        public async Task<Stock?> UpdateAsync(int id, Stock stockModel)
        {
            Stock? existingStock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);

            if (existingStock == null)
            {
                return null;
            }

            existingStock.Symbol = stockModel.Symbol;
            existingStock.CompanyName = stockModel.CompanyName;
            existingStock.Purchase = stockModel.Purchase;
            existingStock.LastDiv = stockModel.LastDiv;
            existingStock.Industry = stockModel.Industry;
            existingStock.MarketCap = stockModel.MarketCap;

            await _context.SaveChangesAsync();

            return stockModel;
        }
    }
}