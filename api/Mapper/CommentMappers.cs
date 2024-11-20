using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Mapper
{
    public class CommentMappers
    {
        public static CommentDto ToCommentDto(this comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                CreatedBy = commentModel.CreatedBy,
                StockId = commentModel.StockId
            };
        }

        public static comment ToCommentFromCreate(this CreateCommentDto commentDto, int StockId)// one to many relationship
        {
            return new comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = commentDto.StockId
            };
        }

        public static comment ToCommnetFromUpdate(this UpdateCommentDto commentDto)
        {
            return new comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content
            };
        }
    }
}