using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStore2.Dtos.User;
using bookStore2.Models;

namespace bookStore2.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this User user){
            return new UserDto{
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
        }
        public static User ToUserFromCreateDto(this CreateUserDto userDto){
            return new User{
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = userDto.Password
            };
        }
        public static User ToUserFromUpdateDto(this UpdateUserDto userDto){
            return new User{
                Username = userDto.Username,
                Email = userDto.Email
            };
        }
    }
}