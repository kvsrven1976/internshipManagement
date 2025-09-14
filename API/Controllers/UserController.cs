using API.Data;
using Microsoft.AspNetCore.Mvc;
using static API.Dtos.DtoRegister;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly AppDbContext _context;
        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegisterDto request)
        {
            // Check if email already exists
            if (_context.Users.Any(u => u.Email == request.Email))
            {
                return BadRequest("Email already exists");
            }

            // Hash password
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new AppUser
            {
                Name = request.Name,
                Mobile = request.Mobile,
                Email = request.Email,
                Password = passwordHash,
                Role = request.Role,
                Status = "active"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();


            // Registration logic will go here
            return Ok(new { message = "User registered successfully : ", user.Id });
        }

        [HttpPost("ListUser")]
        public async Task<IActionResult> ListUser()
        {
            var users = await _context.Users.Select(u => new
            {
                u.Id,
                u.Name,
                u.Mobile,
                u.Email,
                u.Role,
                u.Status,
                u.CreatedAt,
                u.UpdatedAt
            }).ToListAsync();
            return Ok(users);
        }

    }
}
