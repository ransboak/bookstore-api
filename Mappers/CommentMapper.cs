using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStore.Dtos.Comment;
using bookStore.Models;

namespace bookStore.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto{
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content
            };
        }
        public static Comment ToCommentFromCreateDto(this CreateCommentDto commentDto)
        {
            return new Comment{
                Title = commentDto.Title,
                Content = commentDto.Content
            };
        }
    }
}