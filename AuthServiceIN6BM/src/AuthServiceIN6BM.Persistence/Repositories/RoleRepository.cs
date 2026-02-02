using AuthServiceIN6BM.Domain.Entities;
using AuthServiceIN6BM.Domain.Interface;
using AuthServiceIN6BM.Persistence.Data;
using AuthServiceIN6BM.Presistence.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthServiceIN6BV.Persistence.Repositories;

public class RoleRepository(ApplicationDBContext context) : IRoleRepository
{
    public async Task<Role?> GetByNameAsync(string name)
    {
        return await context.Roles.FirstOrDefaultAsync(r => r.Name == name);
    }

    public async Task<int> CountUserInRoleAsync(string roleName)
    {
        return await context.UserRole
            .Include(ur => ur.Role)
            .Where(ur => ur.Role.Name == roleName)
            .Select(ur => ur.UserId)
            .Distinct()
            .CountAsync();
    }

    public async Task<IReadOnlyList<User>> GetUsersByRoleAsync(string roleName)
    {
        var users = await context.Users
            .Include(u => u.UserProfile)
            .Include(u => u.UserEmail)
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .Where(u => u.UserRoles.Any(ur => ur.Role.Name == roleName))
            .ToListAsync();
        return users;
    }

    public async Task<IReadOnlyList<string>> GetUserRoleNameAsync(string userId)
    {
        var roles = await context.UserRole
            .Include(ur => ur.Role)
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.Role.Name)
            .ToListAsync();
        return roles;
    }
}

