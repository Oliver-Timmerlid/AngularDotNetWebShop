﻿using API.Dtos;
using API.Models;

namespace API.Data.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(int id);
        Task<UserDto> CreateAsync(UserCreateDto userCreateDto);
        Task<UserDto> UpdateAsync(UserDto userDto);
        Task<bool> DeleteAsync(int id);
        Task<Cart> GetCartAsync(int userId);
        Task<User?> LoginAsync(UserLoginDto loginDto);
    }
}
