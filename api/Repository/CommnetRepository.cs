using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Properties
{
    public class CommnetRepository: ICommnetRepository
    {
        private readonly DataContext _Context;

        public CommnetRepository(DataContext context)
        {
            _Context = context; 
        }

        public async Task<comment> CreateAsync(comment commentModel)
        {
          await _Context.comments.AddAsync(commentModel);
          await _Context.SaveChanges();
          return commentModel;
        }

        public async Task<comment> DeleteAsync(int id)
        {
            var comment = await  _Context.comments.FirstOrDefault(c => c.Id == id);
            if(comment == null)
            {
                return null;
            }
            _Context.comments.Remove(comment);
            await _Context.SaveChanges();
            return comment;
        }

        public async Task<List<comment>> GetAllAsync()
        {
            return await _Context.comments.ToListAsync();
        }

        public async Task<comment> GetByIdAsync(int id)
        {
            return await _Context.comments.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<comment> UpdateAsync(int id, Comment commentModel)
        {
            var existingComment = await _Context.commnets.FirstOrDefaultAsync(c => c.Id == id);
            
            if(existingComment == null)
            {
                return null;
            }

            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;

            await _Context.SaveChanges();

            return existingComment;
        }
    }
}