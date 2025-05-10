using api.Dtos.Comment;
using api.Dtos.Stock.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId
            };
        }

        public static Comment ToCommentModel(this CreateCommentRequestDto commentRequestDto, int stockId)
        {
            return new Comment
            {
                Title = commentRequestDto.Title,
                Content = commentRequestDto.Content,
                StockId = stockId
            };
        }
    }
}