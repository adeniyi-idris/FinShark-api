using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<stock>> GetAllAsync(QueryObject query);

        Task<stock> GetByIdAsync(int id);

        Task<stock?> GetBySymbolAsync(string symbol);

        Task<stock> CreateAsync(stock stockModel);

        Task<stock> UpdateAsync(int id, UpdateStockRequestDto stockDto);

        Task<stock> DeleteAsync(int id);

        Task<bool> StockExist(int id);
    }
}