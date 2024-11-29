using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public class ICommentRepository
    {
        Task<List<comment>> GetAllAsync();
        Task<comment> GetByIdAsync();
        Task<comment> CreateAsync(comment commentModel);
        Task<comment> UpdateAsync(int id, comment commentModel);
        Task<comment> DeleteAsync(int id);
    }
}