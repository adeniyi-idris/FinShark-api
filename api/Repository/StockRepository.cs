using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;

namespace api.Properties
{
    public class StockRepository: IStockRepository
    {
        private readonly DataContext _context;

        public StockRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<stock>> GetAllAsync(QueryObject query)
        {
            //return await _context.stocks.Include(c => c.comments).ToListAsync(); One-to-many using include() method

            //This is for filtering
            var stocks =  _context.stocks.Include(c => c.comments).AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName))
            }

            if(!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol))
            }

            //sorting
            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Symbol", stringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol)
                }
            }

            //Pagination
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
            //return await stocks.ToListAsync();
        }

        public async Task<stock> GetByIdAsync(int id)
        {
            return await _context.stocks.Include(c => c.comments).FirstOrDefaultAsync(s => s.Id == id);

        }

        public async Task<stock> CreateAsync(stock stockModel)
        {
            await _context.stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<stock> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var existingStock = await _context.stocks.FirstOrDefaultAsync(s => s.Id == id);

            if(existingStock == null)
            {
                return null;
            }

            existingStock.Symbol == stockDto.Symbol;
            existingStock.CompanyName == stockDto.CompanyName;
            existingStock.Purchase == stockDto.Purchase;
            existingStock.LastDiv == stockDto.LastDiv;
            existingStock.Industry == stockDto.Industry;
            existingStock.MarketCap == stockDto.MarketCap;

            await _context.SaveChanges();
            
            return existingStock;
        }

        public async Task<stock> DeleteAsync(id)
        {
            await _context.stocks.FirstOrDefaultAsync(s => s.Id == id)

            if(stockModel == null)
            {
                return NotFound();
            }

            _context.stocks.Remove(stockModel);

            await _context.SaveChanges();
            return stockModel;

        }

        public async Task<stock> StockExist(int id)
        {
            return _context.stocks.AnyAsync(s => s.Id == id)
        }

        public Task<stock?> GetBySymbolAsync(string symbol)
        {
            retrun await _context.stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);
        }
    }
}

       