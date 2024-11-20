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

        public async Task<List<stock>> GetAllAsync()
        {
            return await _context.stocks.Include(c => c.comments).ToListAsync();
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
    }
}