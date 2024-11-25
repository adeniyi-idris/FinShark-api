using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Repository
{
    public class PortfolioRepository
    {
        private readonly DataContext _Context;

        public PortfolioRepository(DataContext context)
        {
            _Context = context;
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
             == appUser.Id && x.Stock.Symbol.ToLower() == symbol.ToLower());

             if (portfolioModel == null)
             {
                return null;
             }

             _context.portfolios.Remove(portfolioModel);

             await _context.SaveChanges();

             return portfolioModel;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await _context.portfolios.Where(u => u.AppUserId == user.Id)
            . Select(stock => new Stock{
                Id = stock.StockId,
                Symbol = stock.Stock.Symbol,
                CompanyName = stock.Stock.Symbol,
                Purchase = stock.Stock.Purchase,
                LastDiv = stock.Stock.LastDiv,
                Industry = stock.Stock.Industry,
                MarketCap = stock.Stock.MarketCap
            }).ToListAsync();
        }
    }
}