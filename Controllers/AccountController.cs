using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStore2.Dtos.User;
using bookStore2.Interfaces;
using bookStore2.Mappers;
using bookStore2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bookStore2.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public AccountController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id){
            var user = await _userRepo.GetByIdAsync(id);

            if(user == null) return NotFound();

            return Ok(user.ToUserDto());
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto userDto)
        {
            var userModel = userDto.ToUserFromCreateDto();

            var passwordHasher = new PasswordHasher<User>();
            userModel.PasswordHash = passwordHasher.HashPassword(userModel, userDto.Password);
            await _userRepo.AddAsync(userModel);

            return CreatedAtAction(nameof(GetById), new {id = userModel.Id}, userModel.ToUserDto());
        }
    }
}