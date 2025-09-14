using System;

namespace API.Entities;

public class AppUser
{

    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Mobile { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Role { get; set; } // "student", "teamLeader", "approveAuth"
    public required string Status { get; set; } // e.g., "active", "inactive"
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}
