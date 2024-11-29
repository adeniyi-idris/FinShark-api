using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository
    {
        private readonly DataContext _context;

        public PortfolioRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.portfolios.AddAsync(portfolio);
            await _context.SaveChanges();

            return portfolio;
        }

        public async Task<Portfolio> DeletePortfolio(AppUser appUser, string symbol)
        {
            var portfolioModel =  await _context.portfolios.FirstOrDefaultAsync(x => x.AppUserId
             == appUser.id && x.Stock.Symbol.ToLower() == symbol.ToLower());

             if (portfolioModel == null)
             {
                return null;
             }

             _context.portfolios.Remove(portfolioModel);

             await _context.SaveChanges();

             return portfolioModel;
        }

        public async Task<List<stock>> GetUserPortfolio(AppUser user)
        {
            return await _context.portfolios.Where(u => u.AppUserId == user.Id)
            . Select(stock => new stock{
                Id = stock.stockId,
                Symbol = stock.stock.Symbol,
                CompanyName = stock.stock.Symbol,
                Purchase = stock.stock.Purchase,
                LastDiv = stock.stock.LastDiv,
                Industry = stock.stock.Industry,
                MarketCap = stock.stock.MarketCap
            }).ToListAsync();
        }
    }
}