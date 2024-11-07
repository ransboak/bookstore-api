using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStore.Data;
using bookStore2.Dtos.User;
using bookStore2.Interfaces;
using bookStore2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace bookStore2.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;

        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<User> AddAsync(User user)
        {
            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> DeleteAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null) return null;

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null) return null;

            return user;
        }

        public async Task<User> UpdateAsync(int id, UpdateUserDto userDto)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (existingUser == null) return null;

            existingUser.Username = userDto.Username;
            existingUser.Email = userDto.Email;
            existingUser.PasswordHash = userDto.PasswordHash;

            await _context.SaveChangesAsync();

            return existingUser;

        }

        public async Task<User?> ValidateUser(string username, string password)
        {
            // return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == password);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return null; // User not found
            }

            var passwordHasher = new PasswordHasher<User>();

            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result == PasswordVerificationResult.Success)
            {
                return user;
            }

            return null;
        }
    }
}