using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStore.Data;
using bookStore.Dtos.Comment;
using bookStore.Interfaces;
using bookStore.Mappers;
using bookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace bookStore.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;   
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var existingComment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

            if(existingComment == null) return null;

            _context.Comments.Remove(existingComment);
            
            return existingComment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

            if(comment == null){
                return null;
            }

            return comment;
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentDto commentDto)
        {
            var commentModel = commentDto.ToCommentFromUpdateDto();
            var existingComment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

            if(existingComment == null) return null;

            existingComment.Title = !string.IsNullOrWhiteSpace(commentModel.Title) ? commentModel.Title : existingComment.Title;
            existingComment.Content = !string.IsNullOrWhiteSpace(commentModel.Content) ? commentModel.Content : existingComment.Content;

            await _context.SaveChangesAsync();

            return existingComment;
        }
    }
}