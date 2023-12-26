﻿using AccessService.Model.DTO;
using AccessService.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using AccessService.Data;
using Microsoft.EntityFrameworkCore;

namespace AccessService.Services
{
    public class AccessesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public AccessesService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<UserDetailsDTO> AuthenticateAsync(string Email, string password)
        {
          
            var user = await _context.UserTableModel.FirstOrDefaultAsync(u => u.Email == Email);

            if (user != null)
            {
                if (VerifyPassword(user.Password, password))
                {
                    var role = await _context.UserRoleModel.FirstOrDefaultAsync(r => r.Id == user.RoleId);
                    var userDetailsDTO = new UserDetailsDTO
                    {
                        //Id = user.Id,
                        //Email = user.Email,
                        //RoleId = user.RoleId,
                        RoleName = role?.RoleName,
                        Token = GenerateJwtToken(user)
                    };
                    return userDetailsDTO;
                }
            }
            return null;
        }
        public string GenerateJwtToken(UserTableModelDTO user)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim("userName", user.Name.ToString()), // Add userId claim
            new Claim(ClaimTypes.Email, user.Email), // Add email claim
            new Claim("roleName", Getrole(user.RoleId).ToString()) // Add role claim
             }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private bool VerifyPassword(string storedHashedPassword, string enteredPassword)
        {
            var passwordHasher = new PasswordHasher<UserTableModelDTO>();
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(null, storedHashedPassword, enteredPassword);
            return passwordVerificationResult == PasswordVerificationResult.Success;
        }

        public async Task<UserTableModelDTO> CreateUserAsync(UserTableModelDTO userModel)
        {
            var role = await _context.UserRoleModelDTO.FirstOrDefaultAsync(r => r.Id == userModel.RoleId);
            bool isEmailExist = await _context.UserTableModel.AnyAsync(u => u.Email == userModel.Email);
           
            if (role != null && isEmailExist == false)
            {
                var newUser = new UserTableModelDTO
                {
                    Name = userModel.Name,
                    RoleId = role.Id,
                    Email = userModel.Email,
                    Password = HashPassword(userModel.Password)
                };
                _context.UserTableModel.Add(newUser);
                await _context.SaveChangesAsync();
                return newUser;
            }
            return null;
        }

        private string HashPassword(string password)
        {
            var passwordHasher = new PasswordHasher<UserTableModelDTO>();
            return passwordHasher.HashPassword(null, password);
        }
        
        public string Getrole(int Id)
        {
            if (Id == 0)
            {
                return null;
            }

            else
            {
                var role =  _context.UserRoleModel.FirstOrDefault(x => x.Id == Id);

                if (role != null)
                {
                    return role.RoleName;
                }
                return null;
            }
        }

        public async Task<IEnumerable<UserTableModelDTO>> GetUsersAsync()
        {
            return _context.UserTableModel.ToList();
        }

        public async Task<UserTableModelDTO?> GetUserAsync(int id)
        {
            UserTableModelDTO userTableModelDTO = await _context.UserTableModel.FirstOrDefaultAsync(c => c.Id == id);

            return userTableModelDTO;
        }

        public async Task<UserTableModelDTO?> GetUserAsync(string email)
        {
            UserTableModelDTO userTableModelDTO = await _context.UserTableModel.FirstOrDefaultAsync(c => c.Email.Trim().ToLower() == email.Trim().ToLower());

            return userTableModelDTO;
        }

        public async Task<UserTableModelDTO> UpdateUserAsync(UserTableModelDTO userTableModelDTO)
        {
            UserTableModelDTO userTableModel = await _context.UserTableModel.FirstOrDefaultAsync(c => c.Id == userTableModelDTO.Id);
            var role = await _context.UserRoleModelDTO.FirstOrDefaultAsync(r => r.Id == userTableModelDTO.RoleId);

            if (userTableModel != null && role != null)
            {

                userTableModelDTO.Password = HashPassword(userTableModelDTO.Password);
                _context.Entry<UserTableModelDTO>(userTableModel).CurrentValues.SetValues(userTableModelDTO);
                _context.SaveChanges();
                return userTableModelDTO;
            }

            return null;

        }

    }
}
