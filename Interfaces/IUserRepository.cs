using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStore2.Dtos.User;
using bookStore2.Models;

namespace bookStore2.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();

        Task<User?> GetByIdAsync(int id);

        Task<User> AddAsync(User user);
        Task<User> UpdateAsync(int id, UpdateUserDto userDto);
        Task<User?> DeleteAsync(int id);
        Task<User?> ValidateUser(string username, string password);
    }
}