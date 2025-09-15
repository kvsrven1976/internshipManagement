using API.Data;
using Microsoft.AspNetCore.Mvc;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using API.Interfaces;
using System.IdentityModel.Tokens.Jwt;



namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;

        // ✅ Only ONE constructor
        public UserController(AppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserResponseDto>> Login(LoginDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return Unauthorized("Invalid email or password");
            }

            return new UserResponseDto
            {
                Name = user.Name,
                Token = _tokenService.CreateToken(user)
            };

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
        [Authorize]
        [HttpPost("ListUser")]
        public async Task<IActionResult> ListUser()
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

            // Only allow if user is TeamLeader/Approver or editing their own record
            if (userRole != "TeamLeader" && userRole != "Approver")
            {
                return StatusCode(403, "You are not authorized to get these details");
            }
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
        [Authorize]
        [HttpPost("UpdateUserStatus")]
        public async Task<IActionResult> UpdateUserStatus(UpdateStatusDto request)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId)?.Value ?? "0");

            // Only allow if user is TeamLeader/Approver or editing their own record
            if (userRole != "TeamLeader" && userRole != "Approver" && userId != request.UserId)
            {
                return StatusCode(403, "You are not authorized to update this user details");
            }

            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            user.Status = request.Status;
            user.UpdatedAt = DateTime.Now;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User status updated successfully" });
        }
        [Authorize]
        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser(GetUserByIdDto request)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId)?.Value ?? "0");

            // Only allow if user is TeamLeader/Approver or editing their own record
            if (userRole != "TeamLeader" && userRole != "Approver" && userId != request.UserId)
            {
                return StatusCode(403, "You are not authorized to delete this user details");
            }

            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User deleted successfully" });
        }

        [Authorize]
        [HttpPost("GetUserById")]
        public async Task<IActionResult> GetUserById(GetUserByIdDto request)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId)?.Value ?? "0");

            // Only allow if user is TeamLeader/Approver or editing their own record
            if (userRole != "TeamLeader" && userRole != "Approver" && userId != request.UserId)
            {
                return StatusCode(403, "You are not authorized to get this user details");
            }
            var user = await _context.Users
                .Where(u => u.Id == request.UserId)
                .Select(u => new
                {
                    u.Id,
                    u.Name,
                    u.Mobile,
                    u.Email,
                    u.Role,
                    u.Status,
                    u.CreatedAt,
                    u.UpdatedAt
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }
        [Authorize]
        [HttpPost("EditUser")]
        public async Task<IActionResult> EditUser(EditUserByIdDto request)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var userRole = User.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId)?.Value ?? "0");

            // Only allow if user is TeamLeader/Approver or editing their own record
            if (userRole != "TeamLeader" && userRole != "Approver" && userId != request.UserId)
            {
                return StatusCode(403, "You are not authorized to edit this user");
            }
            // Update only the properties you want
            user.Name = request.Name;
            user.Mobile = request.Mobile;
            user.Email = request.Email;
            user.UpdatedAt = DateTime.Now; // server local time
            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok(new
            {
                user.Id,
                user.Name,
                user.Mobile,
                user.Email
            });
        }

    }


    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class EditUserByIdDto
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

    }

    public class GetUserByIdDto
    {
        public int UserId { get; set; }
    }
    public class UpdateStatusDto
    {
        public int UserId { get; set; }           // Id of the user to update
        public string Status { get; set; } = "";  // new status, e.g., "active" or "inactive"
    }
    public class RegisterDto
    {
        public string Name { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "student"; // default role
        public string Status { get; set; } = "active"; // default

    }
    public class UserResponseDto
    {

        public required string Name { get; set; } = string.Empty;
        public required string Token { get; set; } = string.Empty;
    }
}
